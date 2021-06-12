using System;
using Godot;

public class ScoreMenu : Control
{
    Button StartNextLevelButton { get; set; }
    Button ExitButton { get; set; }

    TextureRect Banana1 { get; set; }
    TextureRect Banana2 { get; set; }
    TextureRect Banana3 { get; set; }
    Control MenuContainer { get; set; }

    public int Score { get; set; } = 2;

    public override void _Ready()
    {
        StartNextLevelButton = (Button)FindNode("StartNextLevelButton");
        ExitButton = (Button)FindNode("ExitButton");
        Banana1 = (TextureRect)FindNode("BananaTextureRect1");
        Banana2 = (TextureRect)FindNode("BananaTextureRect2");
        Banana3 = (TextureRect)FindNode("BananaTextureRect3");
        MenuContainer = (Control)FindNode("MenuContainer");

        StartNextLevelButton.Connect("pressed", this, nameof(OnStartNextLevelButtonPressed));
        ExitButton.Connect("pressed", this, nameof(OnExitButtonPressed));

        Connect("visibility_changed", this, nameof(OnVisbilityChanged));
        MenuContainer.Visible = Visible;
    }

    void OnVisbilityChanged()
    {
        MenuContainer.Visible = Visible;
        Banana1.Visible = Visible;
        Banana2.Visible = Visible;
        Banana3.Visible = Visible;

        Score = LevelsInfo.Instance.bananasCollectedThisRound;
        
        if (Score <= 2) Banana3.SelfModulate = new Color("000000");
        else Banana3.SelfModulate = new Color("FFFFFF");

        if (Score <= 1) Banana2.SelfModulate = new Color("000000");
        else Banana2.SelfModulate = new Color("FFFFFF");
        
        if (Score <= 0) Banana1.SelfModulate = new Color("000000");
        else Banana1.SelfModulate = new Color("FFFFFF");
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
