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

    public override void _Ready()
    {
        right = (Area2D)FindNode("Right");
        left = (Area2D)FindNode("Left");
        top = (Area2D)FindNode("Top");
        bottom = (Area2D)FindNode("Bottom");
        tween = (Tween)FindNode("Tween");
    }

    void OnRightBodyEntered(Node body)
    {
        if (body.Name == "Boomerang")
        {
            tween.InterpolateProperty(this, "position", null, Position - new Vector2(100, 0), 2, trans, ease);
            tween.Start();
        }
    }

    void OnLeftBodyEntered(Node body)
    {
        if (body.Name == "Boomerang")
        {
            tween.InterpolateProperty(this, "position", null, Position + new Vector2(100, 0), 2, trans, ease);
            tween.Start();
        }
    }

    void OnTopBodyEntered(Node body)
    {
        if (body.Name == "Boomerang")
        {
            tween.InterpolateProperty(this, "position", null, Position + new Vector2(0, 100), 2, trans, ease);
            tween.Start();
        }
    }

    void OnBottomBodyEntered(Node body)
    {
        if (body.Name == "Boomerang")
        {
            tween.InterpolateProperty(this, "position", null, Position - new Vector2(0, 100), 2, trans, ease);
            tween.Start();
        }
    }

}
