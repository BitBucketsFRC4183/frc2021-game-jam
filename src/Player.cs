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

    bool changingForce = false;

    public override void _Ready()
    {
        // debug control nodes
        debugContainer = (Control)FindNode("DebugContainer");
        forceLabel = (Label)FindNode("ForceLabel");
        angleLabel = (Label)FindNode("AngleLabel");

        // boomerang and throwing nodes
        boomerang = GetNode<Boomerang>("PlayerSprites/Boomerang");
        forceBar = GetNode<ProgressBar>("ForceBar");
        sprites = GetNode<Node2D>("PlayerSprites");

        forceBar.Hide();
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
                forceBar.Value += forceBar.Step;
                if (forceBar.Value >= forceBar.MaxValue)
                {
                    barIncreasingInPower = false;
                }
            }
            else
            {
                forceBar.Value -= forceBar.Step;
                if (forceBar.Value <= forceBar.MinValue)
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
                boomerang.Throw(sprites.Rotation, (float)forceBar.Value);
                forceBar.Value = 0;
                barIncreasingInPower = true;
                forceBar.Hide();
            }
        }

        forceLabel.Text = $"Force: {forceBar.Value}";
        angleLabel.Text = $"Angle: {sprites.Rotation}";
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionReleased("debug_mode"))
        {
            debugContainer.Visible = !debugContainer.Visible;
        }
    }
}
