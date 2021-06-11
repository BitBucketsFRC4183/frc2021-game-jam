using System;
using Godot;

public class Events : Node 
{

    public static event Action<int> levelCompleted;
    public static event Action nextLevelPressed;
    public static event Action gemCollected;


    ////////////////////////////////////

    public static void publishLevelCompeted(int stars) => levelCompleted?.Invoke(stars);
    public static void publishNextLevelPressed() => nextLevelPressed?.Invoke();
    public static void publishGemCollected() => gemCollected?.Invoke();
}