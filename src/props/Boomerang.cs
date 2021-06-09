using Godot;
using System;

public class Boomerang : KinematicBody2D
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Scott: please give this a proper name and proper parameters i just put this here to have the equation in the codebase somewhere
    public double equation() {
        var x = 1;
        var y = 2;

        var result = 0.1 * y * Math.Sqrt(49 - Math.Pow(y, 2));

        return result;
    }

}
