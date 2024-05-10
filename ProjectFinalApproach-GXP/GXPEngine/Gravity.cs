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
    List<Ball> ballObjects;

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

        ballObjects = game.FindObjectsOfType<Ball>().ToList();

        for (int i = 0; i < ballObjects.Count; i++)
        {
            if (ballObjects != null)
            {
                relativePosition = Position - ballObjects[i].Position;

                if (relativePosition.Length() <= radius + ballObjects[i].radius)
                {
                    ballObjects[i].Velocity.y += 0.3f;
                }
            }
        }
    }

    void MovingPlanet()
    {
        if (Input.GetMouseButton(1))
        {

        }
    }

    void Update()
    {
        MovingPlanet();

        x = Position.x;
        y = Position.y;
        
        PullingGravity();
        //Console.WriteLine(relativePosition.Length());
    }
}

