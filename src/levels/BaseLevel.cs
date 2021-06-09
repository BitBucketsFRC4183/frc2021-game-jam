using Godot;
using System;

public class BaseLevel : Node2D
{
    [Export] public Enums.Levels level;

    public override void _Ready()
    {
        
    }

    void _on_TempNextLvlButton_pressed() {
        Events.publishNextLevelPressed();
        
        FindNode("TempNextLvlButton").QueueFree();
    }

}
