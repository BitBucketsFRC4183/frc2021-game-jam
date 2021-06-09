using System;
using Godot;

public class SceneChanger : Node2D
{
    CanvasLayer canvasLayer;
    CanvasLayer playerCanvasLayer;
    AnimationPlayer animPlayer;
    Tween tween;
    
    public override void _Ready()
    {
        canvasLayer = GetNode<CanvasLayer>("CanvasLayer");
        playerCanvasLayer = GetNode<CanvasLayer>("PlayerCanvasLayer");
        animPlayer = GetNode<AnimationPlayer>("CanvasLayer/AnimationPlayer");
        tween = GetNode<Tween>("Tween");

        Events.nextLevelPressed += OnNextLevel;
    }

    public override void _ExitTree()
    {
        Events.nextLevelPressed -= OnNextLevel;
    }

    async void OnNextLevel()
    {
        // layer to go over the current level
        canvasLayer.Layer = 1;
        
        // set the color of the ColorRect
        animPlayer.Play("nextLevel");
        
        LevelsInfo li = LevelsInfo.Instance;

        // get current level node
        Enums.Levels currentLevelEnum = li.currentLevel;
        String currentLevelName = Enum.GetName(currentLevelEnum.GetType(), (int)currentLevelEnum);
        Node2D currentLevelNode = GetNode<Node2D>("/root/" + currentLevelName);

        // make next level scene and set the position to be above the screen
        Enums.Levels nextLevelEnum = (Enums.Levels) ((int)currentLevelEnum + 1);
        Node2D nextLevelScene = (Node2D) li.levelsList[nextLevelEnum].Instance();
        nextLevelScene.Position = new Vector2(0, -720);
        GetTree().Root.AddChild(nextLevelScene);

        // get the next level scene once added to scenetree
        String nextLevelName = Enum.GetName(nextLevelEnum.GetType(), (int)nextLevelEnum);
        var nextLevelChild = GetNode("/root/" + nextLevelName);

        // get rid of current level (hidden by the canvas layer)
        currentLevelNode.QueueFree();

        // now that current level removed, layer to be below the soon-to-be next level
        canvasLayer.Layer = -1;

        // get the Player node from next level and reparent it to SceneChanger
        var playerNode = nextLevelChild.GetNode("Player");
        nextLevelChild.RemoveChild(playerNode);
        playerCanvasLayer.AddChild(playerNode);

        // move the next level scene down over the current level scene
        tween.InterpolateProperty(nextLevelChild, "position", null, Vector2.Zero, 2);
        
        // TODO: play the charcter walking animation

        tween.Start();
        await ToSignal(tween, "tween_all_completed");

        // after transition is done, move the Player back to next level
        playerCanvasLayer.RemoveChild(playerNode);
        nextLevelChild.AddChild(playerNode);

        // set the colorr rect back to transparent
        animPlayer.Play("start");
    }

}
