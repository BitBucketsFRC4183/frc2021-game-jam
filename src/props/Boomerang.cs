using Godot;
using System;

public class Boomerang : KinematicBody2D
{
    const float Speed = 100f;

    Vector2 Direction { get; set; }
    float Force { get; set; }
    bool Moving { get; set; }

    Node parent;

    VisibilityNotifier2D visibilityNotifier2D;
    public override void _Ready()
    {
        visibilityNotifier2D = GetNode<VisibilityNotifier2D>("VisibilityNotifier2D");
        parent = GetParent();
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        if (Moving)
        {
            Position += Direction * delta * Speed * Force;
        }

        if (!visibilityNotifier2D.IsOnScreen())
        {
            Moving = false;
            Position = Vector2.Zero;
            if (parent != null && GetParent() != null)
            {
                // remove ourselves from whoever we are attached to and add us back to
                // our original parent.
                GetParent().RemoveChild(this);
                parent?.AddChild(this);
            }
        }

    }

    // Scott: please give this a proper name and proper parameters i just put this here to have the equation in the codebase somewhere
    public void Throw(double angle, float force)
    {
        Moving = true;
        Direction = new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)).Normalized();
        Force = force;

        // remove ourselves from the Player node so we don't rotate or move with it
        // while being thrown
        // then add ourselves to the root scene tree
        var globalPosition = GlobalPosition;
        var root = GetTree().Root;
        parent?.RemoveChild(this);
        root.AddChild(this);
        GlobalPosition = globalPosition;

        GD.Print($"LAUNCHED AT force: {force}, direction: {Direction} !!!");
    }

}
