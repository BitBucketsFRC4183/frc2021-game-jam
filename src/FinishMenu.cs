using Godot;
using System;

public class FinishMenu : Menu
{
    GridContainer GridContainer { get; set; }
    Texture bananaTexture;

    public override void _Ready()
    {
        base._Ready();
        GridContainer = (GridContainer)FindNode("GridContainer");

        bananaTexture = GD.Load<Texture>("res://assets/banana.png");
        foreach (Node child in GridContainer.GetChildren())
        {
            GridContainer.RemoveChild(child);
            child.QueueFree();
        }
    }

    protected override void OnVisbilityChanged()
    {
        base.OnVisbilityChanged();
        if (Visible)
        {
            for (int i = 0; i < LevelsInfo.Instance.bananasCollectedTotal; i++)
            {
                GridContainer.AddChild(new TextureRect()
                {
                    Texture = bananaTexture,
                    Modulate = Colors.Yellow
                });
            }
        }
    }

    protected override void OnNewGameButtonPressed()
    {
        Events.PublishRestartGame();
    }

}
