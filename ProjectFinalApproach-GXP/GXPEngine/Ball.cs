using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

internal class Ball : AnimationSprite
{
    Sound boom = new Sound("Boom.wav");
    public Vec2 Position;
    public Vec2 Velocity;
    public int radius;
    public int speed = 9;


    public Ball() : base("Ball.png", 1, 1, -1, false, false)
    {
        Initialize();
    }

    public Ball(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        Initialize();
    }

    void Initialize()
    {
        radius = 16;
        SetOrigin(width / 2, height / 2);
        SetScaleXY(radius * 2, radius * 2);

        Position = new Vec2(100, game.height / 2);
        Velocity = new Vec2(4, 0);
        Velocity = Velocity.Normalized() * speed;
    }

    void BoundaryWrap()
    {
        if (Position.x - radius > game.width)
        {
            Position.x = 0;
        }

        if (Position.y - radius > game.height)
        {
            Position.y = 0;
        }

        if (Position.x + radius < 0)
        {
            Position.x = game.width;
        }

        if (Position.y + radius < 0)
        {
            Position.y = game.height;
        }
    }

    Vec2 planetDistance;
    Planet planet;

    void CheckPlanetCollision()
    {

        planet = game.FindObjectOfType<Planet>();


        if (planet != null)
        {
            planetDistance = Position - planet.Position;

            if (planetDistance.Length() <= radius + planet.radius)
            {
                Velocity.Reflect(planetDistance);
                boom.Play().Volume = 0.2f;
            }
        }
    }

    void Update()
    {
        x = Position.x;
        y = Position.y;

        Position += Velocity;

        BoundaryWrap();
        CheckPlanetCollision();

        if (Input.GetKeyDown(Key.SPACE))
        {
            Velocity = Velocity * -1;
        }
    }
}