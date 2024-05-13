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
    Vec2 PlayerPos;
    float Angle;
    bool DestroyObject = true;
    bool tester;

    //tweak the speed of the ball here!!
    public int speed = 5;

    public Ball(float pAngle, Vec2 pPlayerPos, bool pTester = false) : base("Ball.png", 1, 1, -1, false, false)
    {
        tester = pTester;
        Angle = pAngle;
        PlayerPos = pPlayerPos;
        if(!tester) InitializeNormal();
        else InitializeTester();
        //I made a different Initialize funtion because this ones position and Velocity angle is determent by the player. the normal initialize method still works the same

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

    void InitializeNormal() // 
    {
        SetOrigin(width / 2, height / 2);
        SetColor(255,0,0);
        //scale = 0.5f;
        Position = PlayerPos;
        Velocity = new Vec2(0, -3);
        Velocity.SetAngleDegrees(Mathf.Clamp(Angle,-135,-45));
    }
    void InitializeTester() // 
    {
        SetOrigin(width / 2, height / 2);
        SetColor(255, 255, 0);
        scale = 0.75f;
        Position = PlayerPos;
        Velocity = new Vec2(0, -5);
        Velocity.SetAngleDegrees(Mathf.Clamp(Angle, -135, -45));
    }
    //Method to keep the projectile inside the game scene
    void BoundaryWrap()
    {
        if (Position.x - radius > game.width)
        {
            if (DestroyObject) LateDestroy();
            Position.x = 0;
        }

        if (Position.y - radius > game.height)
        {
            if (DestroyObject) LateDestroy();
            Position.y = 0;
        }

        if (Position.x + radius < 0)
        {
            if (DestroyObject) LateDestroy();
            Position.x = game.width;
        }

        if (Position.y + radius < 0)
        {
            if (DestroyObject) LateDestroy();
            else Position.y = game.height;
        }
        //Console.WriteLine(Position);
    }

    Vec2 planetDistance;
    List<Planet> planetObjects;

    void CheckPlanetCollision()
    {

        //find planet GameObjects
        planetObjects = game.FindObjectsOfType<Planet>().ToList();
        for (int i = 0; i < planetObjects.Count; i++)
        {
            //if there is a planet in the scene....
            if (planetObjects.Count != 0)
            {
                //get the distance between the planet and the ball
                planetDistance = Position - planetObjects[i].Position;

                //if that distance is less than the radius sum (collision)
                if (planetDistance.Length() <= radius + planetObjects[i].radius)
                {
                    //reflect the velocity in the planetDistance vector (which is also the normal)
                    Velocity.Reflect(planetDistance);
                    //if (!tester) boom.Play().Volume = 0.2f;
                }
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

        if (tester) alpha -= 0.01f;
        if (alpha <= 0) alpha = 0;
        //hold down space to invert the velocity, sending the ball backwards

        if (Input.GetKeyDown(Key.SPACE))
        {
            Velocity = Velocity * -1;
        }
    }
}