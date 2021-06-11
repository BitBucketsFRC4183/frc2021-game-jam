using System;
using Godot;

public class Player : Node2D
{
    Boomerang boomerang;
    Tween tween;
    ProgressBar forceBar;
    Node2D sprites;
    bool barIncreasingInPower = true;

    double forceLevel = 0;
    double maxForceLevel = 10;

    bool changingForce = false;

    public override void _Ready()
    {
        boomerang = GetNode<Boomerang>("Boomerang");
        tween = GetNode<Tween>("Tween");
        forceBar = GetNode<ProgressBar>("ForceBar");
        sprites = GetNode<Node2D>("PlayerSprites");
    }

    public override void _PhysicsProcess(float delta)
    {
        var direction = GetGlobalMousePosition() - sprites.Position;
        sprites.Rotation = direction.Angle() + Mathf.Deg2Rad(90);

        if (Input.IsMouseButtonPressed(1))
        {
            changingForce = true;

            if (barIncreasingInPower)
            {
                forceLevel += 0.1;
                if (forceLevel >= maxForceLevel)
                {
                    barIncreasingInPower = false;
                }
            }
            else
            {
                forceLevel -= 0.1;
                if (forceLevel <= 0)
                {
                    barIncreasingInPower = true;
                }
            }
        }
        else
        {
            if (changingForce)
            {
                changingForce = false;
                boomerang.equation(sprites.Rotation, forceLevel);
                forceLevel = 0;
                barIncreasingInPower = true;
            }
        }

        tween.InterpolateProperty(forceBar, "value", null, forceLevel, (float)0.1);
        tween.Start();

    }
}
