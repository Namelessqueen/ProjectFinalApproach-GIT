using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;
using GXPEngine;
using System.Runtime.CompilerServices;


internal class Gravity : AnimationSprite
{
    public Vec2 Position;
    public Vec2 Velocity;
    int radius;

    public Gravity() : base("Force.png", 1, 1, -1, false, false)
    {
        Initialize();
    }

    public Gravity(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        Initialize();
    }

    void Initialize()
    {
        radius = 64;
        SetOrigin(width / 2, height / 2);
        SetScaleXY(radius * 2, radius * 2);

        Position = new Vec2(game.width - (1.5f * radius), game.height / 2);
    }

    Vec2 relativePosition;
    Ball ball;

    void PullingGravity()
    {

        ball = game.FindObjectOfType<Ball>();


        if (ball != null)
        {
            relativePosition = Position - ball.Position;

            if (relativePosition.Length() <= radius + ball.radius)
            {
                ball.Velocity.y += 0.3f;
            }
        }
    }

    void Update()
    {
        x = Position.x;
        y = Position.y;

        PullingGravity();
        Console.WriteLine(relativePosition.Length());
    }
}

