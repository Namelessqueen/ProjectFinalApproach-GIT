using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;
using GXPEngine;
using System.Runtime.CompilerServices;
using System.IO.Ports;


internal class Gravity : AnimationSprite
{
    public Vec2 Position;
    public Vec2 Velocity;
    Vec2 mousePos;
    Vec2 PivitPoint;
    int radius;
    float RotClampMin, RotClampMax;

    //tweak the strength of the gravity field here!!
    float gravityStrength;

    public Gravity(TiledObject obj = null) : base("Force.png", 1, 1, -1, false, false)
    {
        Initialize(obj);
    }

    public Gravity(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        Initialize(obj);
       
    }

    void Initialize(TiledObject obj)
    {
        radius = 125;
        SetOrigin(width / 2, height / 2);
        SetScaleXY(radius * 2, radius * 2);
        float PivitX = obj.GetFloatProperty("PivitX", 0);
        float PivitY = obj.GetFloatProperty("PivitY", 0);
        RotClampMin = obj.GetFloatProperty("RotClampMin", 0);
        RotClampMax = obj.GetFloatProperty("RotClampMax", 0);
        gravityStrength = obj.GetFloatProperty("gravityStrength", 0.025f);
        PivitPoint = new Vec2(PivitX, PivitY);


        //Console.WriteLine("the X:{0} the Y:{1} and the position{2}", x, y, Position);
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
            ballObjects[I].Velocity.x -= gravityStrength * ballObjects[I].Velocity.Length() * 0.11f * Time.deltaTime;

        }

        if (moveRight)
        {
            ballObjects[I].Velocity.x += gravityStrength * ballObjects[I].Velocity.Length() * 0.11f * Time.deltaTime; 
           
        }

        if (moveUp)
        {
            ballObjects[I].Velocity.y -= gravityStrength * ballObjects[I].Velocity.Length() * 0.11f * Time.deltaTime;
        }

        if (moveDown)
        {
            ballObjects[I].Velocity.y += gravityStrength * ballObjects[I].Velocity.Length() * 0.11f * Time.deltaTime;
        }

        ballObjects[I].Velocity = ballObjects[I].Velocity.Normalized() * ballObjects[I].speed;
    }


    void MovingPlanet()
    {
        //track mouse movement
        mousePos = new Vec2(Input.mouseX, Input.mouseY);

        Vec2 rotationMouse = PivitPoint - mousePos;
        float rotation = Mathf.Clamp(rotationMouse.GetAngleDegrees(), RotClampMin, RotClampMax);
        Vec2 dist = Position - mousePos;

        //if (Input.GetKeyDown(Key.P)) Console.WriteLine(rotation);


        //Pressing the right mouse button to change the gravity position
        if (Input.GetMouseButton(1) && dist.Length() < radius)
        {
            //RotateAroundSetDegrees is a new Vec2 void that rotates around a point to a set angle. Using this to match the gravity angle with the one of the mouse
            Position.RotateAroundSetDegrees(PivitPoint, rotation);
        }
    }

    void Update()
    {
        //first make sure that the start position is the same as the one in Tiled, since the Initialize void is not updated before 
        Position = new Vec2(x, y);
        MovingPlanet();

        x = Position.x;
        y = Position.y;

        CheckOverlap();
       
    }
}



