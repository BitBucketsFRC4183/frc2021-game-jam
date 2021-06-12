using Godot;
using System;

public class BaseLevel : Node2D
{
    [Export] public Enums.Levels level;

    Menu Menu { get; set; }
    ScoreMenu ScoreMenu { get; set; }
    FinishMenu FinishMenu { get; set; }
    Control HUDContainer { get; set; }

    public override void _Ready()
    {
        Menu = GetNode<Menu>("Menu");
        ScoreMenu = GetNode<ScoreMenu>("ScoreMenu");
        FinishMenu = GetNode<FinishMenu>("FinishMenu");
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

        var levels = Enum.GetValues(typeof(Enums.Levels));
        if (LevelsInfo.Instance.currentLevel == (Enums.Levels)levels.GetValue(levels.Length - 1))
        {
            // we are on the last level, show the finish menu
            FinishMenu.Visible = true;
        }
        else
        {
            ScoreMenu.Visible = true;
        }
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
