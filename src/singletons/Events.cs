using System;
using Godot;

public class Events : Node 
{

    public static event Action<int> levelCompleted;
    public static event Action nextLevelPressed;

    ////////////////////////////////////

    public static void publishLevelCompeted(int stars) => levelCompleted?.Invoke(stars);
    public static void publishNextLevelPressed() => nextLevelPressed?.Invoke();
}