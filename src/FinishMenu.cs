using Godot;
using System;

public class FinishMenu : Menu
{

    public override void _Ready()
    {
        base._Ready();
    }

    protected override void OnNewGameButtonPressed()
    {
        Events.PublishRestartGame();
    }

}
