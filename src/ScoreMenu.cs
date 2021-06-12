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

    Tween tween { get; set; }
    Button buttonBG { get; set; }

    public int Score { get; set; } = 0;


    public override void _Ready()
    {
        StartNextLevelButton = (Button)FindNode("StartNextLevelButton");
        ExitButton = (Button)FindNode("ExitButton");
        Banana1 = (TextureRect)FindNode("BananaTextureRect1");
        Banana2 = (TextureRect)FindNode("BananaTextureRect2");
        Banana3 = (TextureRect)FindNode("BananaTextureRect3");
        MenuContainer = (Control)FindNode("MenuContainer");
        tween = (Tween)FindNode("Tween");
        buttonBG = (Button)FindNode("ButtonBG");

        StartNextLevelButton.Connect("pressed", this, nameof(OnStartNextLevelButtonPressed));
        ExitButton.Connect("pressed", this, nameof(OnExitButtonPressed));

        Connect("visibility_changed", this, nameof(OnVisbilityChanged));
        buttonBG.Visible = MenuContainer.Visible = Visible;

    }

    void OnVisbilityChanged()
    {
        if (Visible)
        {
            MenuContainer.RectPosition = new Vector2(0, -160);
            buttonBG.RectPosition = new Vector2(30, -160);
            tween.InterpolateProperty(MenuContainer, "rect_position", null, new Vector2(0, 305), 1, Tween.TransitionType.Bounce, Tween.EaseType.Out);
            tween.InterpolateProperty(buttonBG, "rect_position", null, new Vector2(30, 283), 1, Tween.TransitionType.Bounce, Tween.EaseType.Out);
        }
        MenuContainer.Visible = Visible;
        buttonBG.Visible = Visible;
        if (Visible) tween.Start();

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
