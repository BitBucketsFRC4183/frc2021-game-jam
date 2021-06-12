using Godot;
using System;

public class Boomerang : KinematicBody2D
{
    const float Speed = 100f;

    Vector2 Direction { get; set; }
    public float Force { get; set; }
    bool Moving { get; set; }

    VisibilityNotifier2D visibilityNotifier2D;
    Path2D path;
    public override void _Ready()
    {
        path = GetNode<Path2D>("Path2D");
        visibilityNotifier2D = GetNode<VisibilityNotifier2D>("VisibilityNotifier2D");
        visibilityNotifier2D.Connect("viewport_exited", this, nameof(OnVisibilityNotifier2DViewportExited));
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
        }

    }

    void OnVisibilityNotifier2DViewportExited(Viewport viewport)
    {
        if (GetParent() != null && GetParent() == GetTree().Root)
        {
            Events.PublishLevelCompleted();
            QueueFree();
        }
    }

    // TODO: actual boomerang code
    public void Throw(double angle, float force)
    {
        Moving = true;
        Direction = new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)).Normalized();
        Force = force;

        var b = 250;
        var a = b * .5f;
        path.Curve.ClearPoints();

        // go forwards
        for (float y = -b; y < b; y++)
        {
            float x = (float)Math.Sqrt(a * a - ((a * y * a * y) / (b * b)));
            path.Curve.AddPoint(new Vector2(x, y));
        }

        // go backwards
        for (float y = b; y > -b; y--)
        {
            float x = -(float)Math.Sqrt(a * a - ((a * y * a * y) / (b * b)));
            path.Curve.AddPoint(new Vector2(x, y));
        }

        // remove ourselves from the Player node so we don't rotate or move with it
        // while being thrown
        // then add ourselves to the root scene tree
        var globalPosition = GlobalPosition;
        var root = GetTree().Root;
        GetParent()?.RemoveChild(this);
        root.AddChild(this);
        GlobalPosition = globalPosition;
        path.GlobalPosition = new Vector2(globalPosition.x, globalPosition.y - b);

        GD.Print($"LAUNCHED AT force: {force}, direction: {Direction} !!!");
    }

}
