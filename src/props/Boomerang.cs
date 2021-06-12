using Godot;
using Godot.Collections;
using System;

public class Boomerang : Path2D
{
    public float Force { get; set; }

    Tween tween;
    Path2D path;
    PathFollow2D pathFollow2D;

    public override void _Ready()
    {
        path = this;
        tween = GetNode<Tween>("Tween");
        pathFollow2D = GetNode<PathFollow2D>("PathFollow2D");
    }

    async public void Throw(double angle, float force)
    {
        Force = force;

        Mathf.Clamp((float)Force, (float)0.1, (float)1);
        ((BoomerangBody)FindNode("BoomerangBody")).Force = this.Force;

        var b = 300 * Force;
        var a = b * .8f;
        path.Curve.ClearPoints();

        // go forwards
        for (float y = -b; y < b; y += 2)
        {
            float x = (float)Math.Sqrt(a * a - ((a * y * a * y) / (b * b)));
            path.Curve.AddPoint(new Vector2(x, y));
        }

        // go backwards
        for (float y = b; y > -b; y -= 2)
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
        path.GlobalPosition = new Vector2(globalPosition.x, globalPosition.y + (angle > Math.PI ? (b - 32) : -(b - 32)));
        path.Rotation = (float)angle;

        var direction = new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)).Normalized();
        GD.Print($"LAUNCHED AT force: {force}, direction: {direction} !!!");

        var timeBoomerangTakes = 10 * (1 / force) / 10;

        tween.InterpolateProperty(
            pathFollow2D, "unit_offset", 0.5, 0, timeBoomerangTakes / 2, Tween.TransitionType.Linear, Tween.EaseType.InOut
        );
        tween.Start();

        await ToSignal(tween, "tween_completed");

        tween.InterpolateProperty(
            pathFollow2D, "unit_offset", 0, -0.5, timeBoomerangTakes / 2, Tween.TransitionType.Linear, Tween.EaseType.InOut
        );
        tween.Start();

        await ToSignal(tween, "tween_completed");

        Events.PublishLevelCompleted();
        QueueFree();

    }

}
