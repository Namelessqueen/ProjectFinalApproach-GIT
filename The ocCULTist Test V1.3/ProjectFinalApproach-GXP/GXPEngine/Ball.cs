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
    public bool tester;
    string imageFile;
    int cols;
    int rows;
    int ballRad = 20; //This is because the goat sprite uses a lot of free space around the sphere. for calc use this as raduis

    //tweak the speed of the ball here!!
    public int speed = 1;

    public Ball(string pImageFile, int _cols, int _rows, float pAngle, Vec2 pPlayerPos, bool pTester = false) : base(pImageFile, _cols, _rows, -1, false, false)
    {
        imageFile = pImageFile;
        tester = pTester;
        Angle = pAngle;
        PlayerPos = pPlayerPos;
        cols = _cols;
        rows = _rows;
        if (!tester) InitializeNormal();
        else InitializeTester();
        //I made a different Initialize funtion because this ones position and Velocity angle is determent by the player. the normal initialize method still works the same

    }


    void InitializeNormal() // 
    {
        SetCycle(0, 1);
        _cols = 7;
        _rows = 4;
        SetOrigin(width / 2, height / 2);
        scale = 0.75f;
        radius = ballRad;
        Position = PlayerPos;
        Velocity = new Vec2(0, -speed);
        Velocity.SetAngleDegrees(Angle);
    }
    void InitializeTester() // 
    {
        _cols = 1;
        _rows = 1;
        SetOrigin(width / 2, height / 2);
        scale = 0.75f;
        Position = PlayerPos;
        Velocity = new Vec2(0, -speed*1.5f);
        Velocity.SetAngleDegrees(Angle);

        
    }


    void Update()
    {
        x = Position.x;
        y = Position.y;

        Position += Velocity * Time.deltaTime;
        BoundaryWrap();
        CheckPlanetCollision();

        //Change the transparity off the test ball object
        if (tester) alpha -= 0.01f;
        if (alpha <= 0) alpha = 0;

        if(((MyGame)game).success == true)
        {
            Destroy();
        }

        Animate(0.04f * Time.deltaTime);
    }

    //Method to keep the projectile inside the game scene
    void BoundaryWrap()
    {
        if (Position.x - radius > game.width)
        {
            if (DestroyObject) LateDestroy();
            Position.x = 0;
            if (!tester)
            {
                ((MyGame)game).deathCount--;
                Console.WriteLine("Attempts left: {0}", ((MyGame)game).deathCount);
            }
        }

        if (Position.y - radius > game.height)
        {
            if (DestroyObject) LateDestroy();
            Position.y = 0;
            if (!tester)
            {
                ((MyGame)game).deathCount--;
                Console.WriteLine("Attempts left: {0}", ((MyGame)game).deathCount);
            }
        }

        if (Position.x + radius < 0)
        {
            if (DestroyObject) LateDestroy();
            Position.x = game.width;
            if (!tester)
            {
                ((MyGame)game).deathCount--;
                Console.WriteLine("Attempts left: {0}", ((MyGame)game).deathCount);
            }
        }

        if (Position.y + radius < 0)
        {
            if (DestroyObject) LateDestroy();
            else Position.y = game.height;
            if (!tester)
            {
                ((MyGame)game).deathCount--;
                Console.WriteLine("Attempts left: {0}", ((MyGame)game).deathCount);
            }
        }
        //Console.WriteLine(Position);
    }

    Vec2 planetDistance;
    List<Planet> planetObjects;

    int crashTimer = 0;
    public void Crash()
    {
        crashTimer += Time.deltaTime;
        Velocity = new Vec2 (0, 0);
        SetCycle(0, 9);
        if (crashTimer > 200)
        { 
            Console.WriteLine();
             if (!tester)
            {
                Console.WriteLine("Attempts left: {0}", ((MyGame)game).deathCount);
              
                ((MyGame)game).deathCount--;
            }
            crashTimer = 0;
            Destroy();
        }
        
    }


    int passTimer = 0;
    public void Pass()
    {
        passTimer += Time.deltaTime;
        Velocity = new Vec2(0, 0);
        SetCycle(10, 19);
        if (passTimer > 445)
        {
            if (!tester)
            {
                ((MyGame)game).PassLevel();
            }
            passTimer = 0;
            Destroy();
        }
    }

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
                //Console.WriteLine(planetDistance.Length());

                //if that distance is less than the radius sum (collision)
                if (planetDistance.Length() <= radius + planetObjects[i].radius)
                {
                    //The attempt has been used up
                    Crash();             
                }
            }
        }
    }

}