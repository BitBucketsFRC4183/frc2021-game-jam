using Godot;
using System;

public class Menu : Control
{
    Button NewGameButton { get; set; }
    Button ExitButton { get; set; }
    Control MenuContainer { get; set; }

    public override void _Ready()
    {
        NewGameButton = (Button)FindNode("NewGameButton");
        ExitButton = (Button)FindNode("ExitButton");
        MenuContainer = (Control)FindNode("MenuContainer");

        Connect("visibility_changed", this, nameof(OnVisbilityChanged));

        NewGameButton.Connect("pressed", this, nameof(OnNewGameButtonPressed));
        ExitButton.Connect("pressed", this, nameof(OnExitButtonPressed));

        MenuContainer.Visible = Visible;
    }

    void OnVisbilityChanged()
    {
        MenuContainer.Visible = Visible;
    }

    protected virtual void OnNewGameButtonPressed()
    {
        Events.PublishStartGame();
    }

    void OnExitButtonPressed()
    {
        GetTree().Quit();
    }

}
