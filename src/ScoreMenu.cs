using Godot;
using System;

public class ScoreMenu : Control
{
    Button StartNextLevelButton { get; set; }
    Button ExitButton { get; set; }

    TextureRect BananaTextureRect1 { get; set; }
    TextureRect BananaTextureRect2 { get; set; }
    TextureRect BananaTextureRect3 { get; set; }
    Control MenuContainer { get; set; }

    public int Score { get; set; } = 2;

    public override void _Ready()
    {
        StartNextLevelButton = (Button)FindNode("StartNextLevelButton");
        ExitButton = (Button)FindNode("ExitButton");
        BananaTextureRect1 = (TextureRect)FindNode("BananaTextureRect1");
        BananaTextureRect2 = (TextureRect)FindNode("BananaTextureRect2");
        BananaTextureRect3 = (TextureRect)FindNode("BananaTextureRect3");
        MenuContainer = (Control)FindNode("MenuContainer");

        StartNextLevelButton.Connect("pressed", this, nameof(OnStartNextLevelButtonPressed));
        ExitButton.Connect("pressed", this, nameof(OnExitButtonPressed));

        Connect("visibility_changed", this, nameof(OnVisbilityChanged));
        MenuContainer.Visible = Visible;
    }

    void OnVisbilityChanged()
    {
        MenuContainer.Visible = Visible;
        BananaTextureRect1.Visible = Score >= 1;
        BananaTextureRect2.Visible = Score >= 2;
        BananaTextureRect3.Visible = Score >= 3;
    }

    void OnStartNextLevelButtonPressed()
    {
        Events.PublishStartNextLevel();
    }

    void OnExitButtonPressed()
    {
        GetTree().Quit();
    }

}
