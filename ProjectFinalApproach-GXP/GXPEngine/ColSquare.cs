using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

internal class ColSquare : AnimationSprite
{
    Vec2 Position;
    List<Ball> BallObjects;
    public ColSquare() : base("Square2.png", 1, 1, -1)
    {
        Initialize();
    }
    public ColSquare(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        Initialize();
    }

    void Initialize()
    {
        SetOrigin(width / 2, height / 2);
    }

    void Update()
    {
        Console.WriteLine("SQUAAAAAAAAAAARE");
        Position = new Vec2(x, y);
        CheckBallCollision();
    }

    void CheckBallCollision()
    {
        //Finds all the ball objects in the scene
        BallObjects = game.FindObjectsOfType<Ball>().ToList();
        for (int i = 0; i < BallObjects.Count; i++)
        {
            //Get the distance between the ball and the wall. In absolute so it is always a positive number which means less methods
            float distX = Mathf.Abs(Position.x - BallObjects[i].Position.x);
            float distY = Mathf.Abs(Position.y - BallObjects[i].Position.y);

            //checks when that distance is less than the radius of the ball and the size of the wall square
            if (distX < BallObjects[i].radius + width / 2 && distY < BallObjects[i].radius + width / 2)
            {
                Console.WriteLine("HIT WALL"); 
            }
        }
    }
}

