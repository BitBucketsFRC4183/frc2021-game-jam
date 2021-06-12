using System;
using Godot;

public class Block : Area2D
{
    Area2D right;
    Area2D left;
    Area2D top;
    Area2D bottom;
    Tween tween;

    Tween.TransitionType trans = Tween.TransitionType.Circ;
    Tween.EaseType ease = Tween.EaseType.Out;

    // stores the boomerang this block has already collided with
    Boomerang boomerangAlreadyCollidedWith;

    public override void _Ready()
    {
        right = (Area2D)FindNode("Right");
        left = (Area2D)FindNode("Left");
        top = (Area2D)FindNode("Top");
        bottom = (Area2D)FindNode("Bottom");
        tween = (Tween)FindNode("Tween");

        Events.levelCompleted += OnLevelCompleted;
    }

    public override void _ExitTree()
    {
        Events.levelCompleted -= OnLevelCompleted;
    }

    void OnRightBodyEntered(Node body)
    {
        if (body.GetInstanceId() == boomerangAlreadyCollidedWith?.GetInstanceId()) return;
        boomerangAlreadyCollidedWith = (Boomerang)body;

        var force = ((Boomerang)body).Force;

        if (body.Name == "Boomerang")
        {
            tween.InterpolateProperty(this, "position", null, Position - new Vector2(10 * force, 0), 2, trans, ease);
            tween.Start();
        }
    }

    void OnLeftBodyEntered(Node body)
    {
        if (body.GetInstanceId() == boomerangAlreadyCollidedWith?.GetInstanceId()) return;
        boomerangAlreadyCollidedWith = (Boomerang)body;

        var force = ((Boomerang)body).Force;

        if (body.Name == "Boomerang")
        {
            tween.InterpolateProperty(this, "position", null, Position + new Vector2(10 * force, 0), 2, trans, ease);
            tween.Start();
        }
    }

    void OnTopBodyEntered(Node body)
    {
        if (body.GetInstanceId() == boomerangAlreadyCollidedWith?.GetInstanceId()) return;
        boomerangAlreadyCollidedWith = (Boomerang)body;

        var force = ((Boomerang)body).Force;

        if (body.Name == "Boomerang")
        {
            tween.InterpolateProperty(this, "position", null, Position + new Vector2(0, 10 * force), 2, trans, ease);
            tween.Start();
        }
    }

    void OnBottomBodyEntered(Node body)
    {
        if (body.GetInstanceId() == boomerangAlreadyCollidedWith?.GetInstanceId()) return;
        boomerangAlreadyCollidedWith = (Boomerang)body;

        var force = ((Boomerang)body).Force;

        if (body.Name == "Boomerang")
        {
            tween.InterpolateProperty(this, "position", null, Position - new Vector2(0, 10 * force), 2, trans, ease);
            tween.Start();
        }
    }

    void OnLevelCompleted()
    {
        // disable hitboxes so nothing happens when player touches them as they move down once the levl ends
        right.GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
        left.GetNode<CollisionShape2D>("CollisionShape2D2").Disabled = true;
        top.GetNode<CollisionShape2D>("CollisionShape2D3").Disabled = true;
        bottom.GetNode<CollisionShape2D>("CollisionShape2D4").Disabled = true;
    }

}
