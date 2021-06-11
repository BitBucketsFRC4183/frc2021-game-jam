using Godot;
using System;

public class BaseLevel : Node2D
{
    [Export] public Enums.Levels level;

    Menu Menu { get; set; }
    Button TempNextLvlButton { get; set; }
    Control HUDContainer { get; set; }

    public override void _Ready()
    {
        Menu = GetNode<Menu>("Menu");
        HUDContainer = (Control)FindNode("HUDContainer");
        TempNextLvlButton = (Button)FindNode("TempNextLvlButton");

        TempNextLvlButton.Connect("pressed", this, nameof(OnTempNextLvlButtonPressed));

        if (!LevelsInfo.Instance.gameStarted)
        {
            ShowMenu();
        }

        Events.startGame += OnStartGame;
    }

    public override void _ExitTree()
    {
        Events.startGame -= OnStartGame;
    }

    private void OnStartGame()
    {
        Menu.Visible = false;
        GetTree().Paused = false;
        TempNextLvlButton.Visible = true;
        HUDContainer.Visible = true;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
        if (@event.IsActionPressed("show_menu"))
        {
            Events.PublishStartGame();
        }
    }

    void ShowMenu()
    {
        // show the menu and pause the game
        Menu.Visible = true;
        GetTree().Paused = true;
        HUDContainer.Visible = false;
    }

    void OnTempNextLvlButtonPressed()
    {
        TempNextLvlButton.Visible = false;
        Events.PublishNextLevelPressed();
    }

}
