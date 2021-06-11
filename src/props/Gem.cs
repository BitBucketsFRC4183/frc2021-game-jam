using Godot;

public class Gem : Node2D
{
    Tween tween;
    Sprite sprite;
    CollisionShape2D collisionShape;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        tween = GetNode<Tween>("Tween");
        sprite = GetNode<Sprite>("Sprite");
        collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");

        tween.InterpolateProperty(sprite, "scale", null, new Vector2(2, 2), (float)0.6, Tween.TransitionType.Cubic, Tween.EaseType.Out);
        tween.InterpolateProperty(sprite, "modulate", null, new Color(0, 0, 0, 0), (float)0.6, Tween.TransitionType.Linear, Tween.EaseType.Out);
    }

    void OnGemBodyEntered() {
        collisionShape.Disabled = true;
        tween.Start();
    }

    void OnTweenTweenCompleted() {
        Events.PublishGemCollected();
        QueueFree();
    }
}
