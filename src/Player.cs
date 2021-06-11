using System;
using Godot;

public class Player : Node2D
{
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
        boomerang = GetNode<Boomerang>("PlayerSprites/Boomerang");
        tween = GetNode<Tween>("Tween");
        forceBar = GetNode<ProgressBar>("ForceBar");
        sprites = GetNode<Node2D>("PlayerSprites");

        forceBar.Hide();

        // TODO: TEMPORARY!
        Events.levelCompleted += OnLevelCompleted;
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

        tween.InterpolateProperty(forceBar, "value", null, forceLevel, (float)0.1);
        tween.Start();

    }

    // TODO: TEMPORARY!
    void OnLevelCompleted()
    {
        Boomerang newBoomerang = (Boomerang)GD.Load<PackedScene>("res://src/props/Boomerang.tscn").Instance();
        newBoomerang.Position = new Vector2(0, -37);
        sprites.AddChild(newBoomerang);
    }
}
