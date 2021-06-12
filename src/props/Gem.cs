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

        this.Connect("body_entered", this, nameof(OnGemBodyEntered));
        this.Connect("area_entered", this, nameof(OnGemAreaEntered));
        tween.Connect("tween_completed", this, nameof(OnTweenTweenCompleted));

        Events.startNextLevel += OnStartNextLevel;
    }

    public override void _ExitTree()
    {
        Events.startNextLevel -= OnStartNextLevel;
    }

    void OnGemBodyEntered(Node body) {
        collisionShape.SetDeferred("disabled", true);
        Events.PublishGemCollected();
        tween.Start();
    }
    void OnGemAreaEntered(Area2D area) {
        collisionShape.SetDeferred("disabled", true);
        Events.PublishGemCollected();
        tween.Start();
    }

    void OnTweenTweenCompleted(Object obj, NodePath key) {
        QueueFree();
    }

    void OnStartNextLevel() {
        collisionShape.SetDeferred("disabled", true);
    }
}
