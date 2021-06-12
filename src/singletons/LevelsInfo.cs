using System.Collections.Generic;
using Godot;

public class LevelsInfo : Node
{
    public static LevelsInfo Instance;

    public bool gameStarted = false;
    public Enums.Levels currentLevel = Enums.Levels.Level1;
    public Dictionary<Enums.Levels, PackedScene> levelsList;

    public override void _Ready()
    {
        Instance = this;

        levelsList = new Dictionary<Enums.Levels, PackedScene>()
        {
            {Enums.Levels.Level1, GD.Load<PackedScene>("res://src/levels/Level1.tscn")},
            {Enums.Levels.Level2, GD.Load<PackedScene>("res://src/levels/Level2.tscn")}
        };

        Events.startGame += OnStartGame;
        Events.endGame += OnEndGame;
    }

    public override void _ExitTree()
    {
        Events.startGame -= OnStartGame;
        Events.endGame -= OnEndGame;
    }

    void OnStartGame()
    {
        gameStarted = true;
    }

    void OnEndGame()
    {
        gameStarted = false;
    }

}
