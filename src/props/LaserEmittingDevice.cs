using Godot;
using System;

public class LaserEmittingDevice : Node2D
{
    [Export] public int BeamLength = 0;

    private Sprite beam;
    private RayCast2D ray1;
    private RayCast2D ray2;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        beam = (Sprite)FindNode("Laser");
        ray1 = (RayCast2D)FindNode("Ray1");
        ray2 = (RayCast2D)FindNode("Ray2");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }
}
