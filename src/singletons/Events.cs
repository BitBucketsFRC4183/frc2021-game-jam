using System;
using Godot;

public class Events : Node
{

    public static event Action startGame;
    public static event Action restartGame;
    public static event Action endGame;
    public static event Action levelCompleted;
    public static event Action startNextLevel;
    public static event Action nextLevelBegun;
    public static event Action gemCollected;


    ////////////////////////////////////

    public static void PublishGemCollected() => gemCollected?.Invoke();
    public static void PublishStartGame() => startGame?.Invoke();
    public static void PublishRestartGame() => restartGame?.Invoke();
    public static void PublishEndGame() => endGame?.Invoke();
    public static void PublishLevelCompleted() => levelCompleted?.Invoke();
    public static void PublishStartNextLevel() => startNextLevel?.Invoke();
    public static void PublishNextLevelBegun() => nextLevelBegun?.Invoke();
}