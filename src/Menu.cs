using Godot;
using System;

public class Menu : Control
{
    Button buttonBG { get; set; }
    Tween tween { get; set; }

    Button NewGameButton { get; set; }
    Button ExitButton { get; set; }
    Control MenuContainer { get; set; }

    public override void _Ready()
    {
        tween = (Tween)FindNode("Tween");
        buttonBG = (Button)FindNode("ButtonBG");
        NewGameButton = (Button)FindNode("NewGameButton");
        ExitButton = (Button)FindNode("ExitButton");
        MenuContainer = (Control)FindNode("MenuContainer");

        Connect("visibility_changed", this, nameof(OnVisbilityChanged));

        NewGameButton.Connect("pressed", this, nameof(OnNewGameButtonPressed));
        ExitButton.Connect("pressed", this, nameof(OnExitButtonPressed));

        buttonBG.Visible = MenuContainer.Visible = Visible;
    }

    void OnVisbilityChanged()
    {
        if (Visible) {
            MenuContainer.RectPosition = new Vector2(0, -160);
            buttonBG.RectPosition = new Vector2(30, -160);
            tween.InterpolateProperty(MenuContainer, "rect_position", null, new Vector2(0, 305), 1, Tween.TransitionType.Bounce, Tween.EaseType.Out);
            tween.InterpolateProperty(buttonBG, "rect_position", null, new Vector2(30, 283), 1, Tween.TransitionType.Bounce, Tween.EaseType.Out);
        }

        buttonBG.Visible = MenuContainer.Visible = Visible;
        if (Visible) tween.Start();
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
