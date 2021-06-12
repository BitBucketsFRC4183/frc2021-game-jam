using System.Collections.Generic;
using Godot;

public class LevelsInfo : Node
{
    public static LevelsInfo Instance;

    public bool gameStarted = false;
    public Enums.Levels currentLevel = Enums.Levels.Level1;
    public Dictionary<Enums.Levels, PackedScene> levelsList;

    public int bananasCollectedThisRound = 0;
    public int bananasCollectedTotal = 0;

    public override void _Ready()
    {
        Instance = this;

        levelsList = new Dictionary<Enums.Levels, PackedScene>()
        {
            {Enums.Levels.Level1, GD.Load<PackedScene>("res://src/levels/Level1.tscn")},
            {Enums.Levels.Level2, GD.Load<PackedScene>("res://src/levels/Level2.tscn")}
        };

        Events.startGame += OnStartGame;
        Events.startGame += Reset;
        Events.restartGame += Reset;
        Events.endGame += OnEndGame;
        Events.nextLevelBegun += OnNextLevelBegun;

        Events.gemCollected += OnGemCollected;
    }

    public override void _ExitTree()
    {
        Events.startGame -= OnStartGame;
        Events.startGame -= Reset;
        Events.restartGame -= Reset;
        Events.endGame -= OnEndGame;
        Events.nextLevelBegun -= OnNextLevelBegun;

        Events.gemCollected -= OnGemCollected;
    }

    void OnStartGame()
    {
        gameStarted = true;
    }

    void OnEndGame()
    {
        gameStarted = false;
    }

    void OnGemCollected()
    {
        bananasCollectedThisRound++;
        bananasCollectedTotal++;
    }

    void Reset()
    {
        currentLevel = Enums.Levels.Level1;
        bananasCollectedThisRound = 0;
        bananasCollectedTotal = 0;
    }

    void OnNextLevelBegun() {
        bananasCollectedThisRound = 0;
    }

}
