using System;
using Godot;

public class Player : Node2D
{
    Control debugContainer;
    Label forceLabel;
    Label angleLabel;

    Boomerang boomerang;
    Tween tween;
    ProgressBar forceBar;
    Node2D sprites;
    bool barIncreasingInPower = true;

    float forceLevel = 0;
    float maxForceLevel = 10;

    bool changingForce = false;

    public override void _Ready()
    {
        // debug control nodes
        debugContainer = (Control)FindNode("DebugContainer");
        forceLabel = (Label)FindNode("ForceLabel");
        angleLabel = (Label)FindNode("AngleLabel");

        // boomerang and throwing nodes
        boomerang = GetNode<Boomerang>("PlayerSprites/Boomerang");
        tween = GetNode<Tween>("Tween");
        forceBar = GetNode<ProgressBar>("ForceBar");
        sprites = GetNode<Node2D>("PlayerSprites");

        forceBar.Hide();

        // TODO: TEMPORARY!
        Events.levelCompleted += OnLevelCompleted;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        Events.levelCompleted -= OnLevelCompleted;
    }

    public override void _PhysicsProcess(float delta)
    {
        var direction = GetGlobalMousePosition() - sprites.GlobalPosition;
        sprites.Rotation = direction.Angle() + Mathf.Deg2Rad(90);

        if (Input.IsMouseButtonPressed(1))
        {
            changingForce = true;
            forceBar.Show();

            if (barIncreasingInPower)
            {
                forceLevel += 0.1f;
                if (forceLevel >= maxForceLevel)
                {
                    barIncreasingInPower = false;
                }
            }
            else
            {
                forceLevel -= 0.1f;
                if (forceLevel <= 0)
                {
                    barIncreasingInPower = true;
                }
            }
        }
        else
        {
            if (changingForce)
            {
                changingForce = false;
                boomerang.Throw(sprites.Rotation, forceLevel);
                forceLevel = 0;
                barIncreasingInPower = true;
                forceBar.Hide();
            }
        }

        forceLabel.Text = $"Force: {forceLevel}";
        angleLabel.Text = $"Angle: {sprites.Rotation}";

        tween.InterpolateProperty(forceBar, "value", null, forceLevel, (float)0.1);
        tween.Start();

    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
        if (@event.IsActionReleased("debug_mode"))
        {
            debugContainer.Visible = !debugContainer.Visible;
        }
    }

    // TODO: TEMPORARY!
    void OnLevelCompleted()
    {
        Boomerang newBoomerang = (Boomerang)GD.Load<PackedScene>("res://src/props/Boomerang.tscn").Instance();
        newBoomerang.Position = new Vector2(0, -37);
        sprites.AddChild(newBoomerang);
    }
}
