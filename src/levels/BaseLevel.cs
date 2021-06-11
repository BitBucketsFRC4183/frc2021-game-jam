using Godot;
using System;

public class BaseLevel : Node2D
{
    [Export] public Enums.Levels level;

    Menu Menu { get; set; }
    ScoreMenu ScoreMenu { get; set; }
    Control HUDContainer { get; set; }

    public override void _Ready()
    {
        Menu = GetNode<Menu>("Menu");
        ScoreMenu = GetNode<ScoreMenu>("ScoreMenu");
        HUDContainer = (Control)FindNode("HUDContainer");

        if (!LevelsInfo.Instance.gameStarted)
        {
            ShowMenu();
        }

        Events.startGame += OnStartGame;
        Events.levelCompleted += OnLevelCompleted;
        Events.startNextLevel += OnStartNextLevel;
    }

    public override void _ExitTree()
    {
        Events.startGame -= OnStartGame;
        Events.levelCompleted -= OnLevelCompleted;
        Events.startNextLevel -= OnStartNextLevel;
    }

    void OnStartGame()
    {
        Menu.Visible = false;
        GetTree().Paused = false;
        HUDContainer.Visible = true;
    }

    void OnLevelCompleted()
    {
        ScoreMenu.Score = 2; // TODO: add in actual score..
        ScoreMenu.Visible = true;
        GetTree().Paused = true;
        HUDContainer.Visible = false;
    }

    void OnStartNextLevel()
    {
        ScoreMenu.Visible = false;
        GetTree().Paused = false;
        HUDContainer.Visible = true;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
        if (@event.IsActionPressed("show_menu"))
        {
            ShowMenu();
        }
    }

    void ShowMenu()
    {
        // show the menu and pause the game
        Menu.Visible = true;
        GetTree().Paused = true;
        HUDContainer.Visible = false;
    }

}
