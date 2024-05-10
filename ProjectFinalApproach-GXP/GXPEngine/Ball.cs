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

    //tweak the speed of the ball here!!
    public int speed = 5;


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

    //Method to keep the projectile inside the game scene
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

        //find planet GameObject
        planet = game.FindObjectOfType<Planet>();

        //if there is a planet in the scene....
        if (planet != null)
        {
            //get the distance between the planet and the ball
            planetDistance = Position - planet.Position;

            //if that distance is less than the radius sum (collision)
            if (planetDistance.Length() <= radius + planet.radius)
            {
                //reflect the velocity in the planetDistance vector (which is also the normal)
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

        //hold down space to invert the velocity, sending the ball backwards

        if (Input.GetKeyDown(Key.SPACE))
        {
            Velocity = Velocity * -1;
        }
    }
}