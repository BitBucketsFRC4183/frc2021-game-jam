using System;
using Godot;

public class SceneChanger : Node2D
{
    CanvasLayer bgCanvasLayer;
    CanvasLayer playerCanvasLayer;
    AnimationPlayer animPlayer;
    Tween tween;
    
    public override void _Ready()
    {
        bgCanvasLayer = GetNode<CanvasLayer>("GreenBGCanvasLayer");
        playerCanvasLayer = GetNode<CanvasLayer>("PlayerCanvasLayer");
        animPlayer = GetNode<AnimationPlayer>("GreenBGCanvasLayer/AnimationPlayer");
        tween = GetNode<Tween>("Tween");

        Events.startNextLevel += OnNextLevel;
    }

    public override void _ExitTree()
    {
        Events.startNextLevel -= OnNextLevel;
    }

    async void OnNextLevel()
    {
        LevelsInfo li = LevelsInfo.Instance;


        // layer goes behind the level, provides green background
        //? we could probably set the clear color of the project and achieve the same w/out the canvas layer but that feels a bit hackish so we'll keep it this way
        bgCanvasLayer.Layer = -1;
        
        // set the color of the ColorRect
        animPlayer.Play("nextLevel");

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

        // get the Player node from next level and reparent it to SceneChanger
        // so that its position doesn't change when the other level elements move
        var playerNode = nextLevelChild.GetNode("Player");
        nextLevelChild.RemoveChild(playerNode);
        playerCanvasLayer.AddChild(playerNode);

        // delete player from current level (fully overlapped by the newly re-parented player)
        currentLevelNode.GetNode("Player").QueueFree();

        // move current level out of screen and next level onto it
        tween.InterpolateProperty(nextLevelChild, "position", null, Vector2.Zero, 2);
        tween.InterpolateProperty(currentLevelNode, "position", null, new Vector2(0, 720), 2);
        playerNode.GetNode<AnimationPlayer>("AnimationPlayer").Play("Walk");

        tween.Start();
        await ToSignal(tween, "tween_all_completed");
        playerNode.GetNode<AnimationPlayer>("AnimationPlayer").Play("Stand");

        // get rid of current level (now off-screen)
        currentLevelNode.QueueFree();

        // after transition is done, move the Player back to next level
        playerCanvasLayer.RemoveChild(playerNode);
        nextLevelChild.AddChild(playerNode);

        // set the colorr rect back to transparent
        animPlayer.Play("start");
    }

}
