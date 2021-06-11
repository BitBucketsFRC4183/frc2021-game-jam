using Godot;
using System;

public class Boomerang : KinematicBody2D
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    // Scott: please give this a proper name and proper parameters i just put this here to have the equation in the codebase somewhere
    public double equation(double direction, double force)
    {

        GD.Print("LAUNCHED AT " + force + "!!!");

        return 0;
    }

}
