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
    Vec2 mousePos;
    int radius;

    //tweak the strength of the gravity field here!!
    float gravityStrength = 0.04f;

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
        radius = 125;
        SetOrigin(width / 2, height / 2);
        SetScaleXY(radius * 2, radius * 2);

        Position = new Vec2(game.width/2, game.height / 4);
    }

    Vec2 ballDistance;
    List<Ball> ballObjects;


    void CheckOverlap()
    {
        //check for ball gameobjects
        ballObjects = game.FindObjectsOfType<Ball>().ToList();

        for (int i = 0; i < ballObjects.Count; i++)
        {
            //if there is a ball in the game scene...
            if (ballObjects[i] != null)
            {
                //get the distance between the gravity field and the ball
                ballDistance = Position - ballObjects[i].Position;

                //if the ball overlaps with the field...
                if (ballDistance.Length() <= radius + ballObjects[i].radius)
                {
                    //alter the velocity accordingly
                    AlterCourse(i);
                }
                else
                {
                    //make sure the speed of the ball remains the same after leaving the gravity field
                    ballObjects[i].Velocity = ballObjects[i].Velocity.Normalized() * ballObjects[i].speed;
                }
            }
        }
    }
    

    bool moveLeft = false;
    bool moveRight = false;
    bool moveUp = false;
    bool moveDown = false;

    void AlterCourse(int i)
    {
        //Probably not the most efficient way to do it, but this is where the ball's velocity gets altered depending on from where it enters the field
        int I = i;
        if (ballObjects[I].Position.x >= Position.x)
        {
            moveLeft = true;
        } else
        {
            moveLeft = false;
        }

        if (ballObjects[I].Position.x < Position.x)
        {
            moveRight = true;
        } else
        {
            moveRight = false;
        }

        if (ballObjects[I].Position.y < Position.y)
        {
            moveDown = true;
        }
        else
        {
            moveDown = false;
        }

        if (ballObjects[I].Position.y >= Position.y)
        {
            moveUp = true;
        }
        else
        {
            moveUp = false;
        }

        if (moveLeft)
        {
            ballObjects[I].Velocity.x -= gravityStrength * ballObjects[I].Velocity.Length();

        }

        if (moveRight)
        {
            ballObjects[I].Velocity.x += gravityStrength * ballObjects[I].Velocity.Length(); 
           
        }

        if (moveUp)
        {
            ballObjects[I].Velocity.y -= gravityStrength * ballObjects[I].Velocity.Length();
        }

        if (moveDown)
        {
            ballObjects[I].Velocity.y += gravityStrength * ballObjects[I].Velocity.Length();
        }

        ballObjects[I].Velocity = ballObjects[I].Velocity.Normalized() * ballObjects[I].speed;
    }


    void MovingPlanet()
    {
        mousePos = new Vec2(Input.mouseX, Input.mouseY);

        Vec2 rotationPoint = new Vec2(game.width / 2, 3*game.height / 4);
        Vec2 rotationMouse =rotationPoint - mousePos;
        Vec2 dist = Position - mousePos;

        if (Input.GetMouseButton(1) && dist.Length() < radius)
        { 
            Position.RotateAroundSetDegrees(rotationPoint, rotationMouse.GetAngleDegrees());
        }
    }

    void Update()
    {
        MovingPlanet();

        x = Position.x;
        y = Position.y;

        CheckOverlap();
    }
}

