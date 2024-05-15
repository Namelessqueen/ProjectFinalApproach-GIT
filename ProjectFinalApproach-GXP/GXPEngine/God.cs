using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

internal class God : AnimationSprite
{
    Vec2 Position;
    List<Ball> BallObjects;
    String nextLevel;
    public God(TiledObject obj = null) : base("Square.png", 1, 1, -1, false, false)
    {
        Initialize(obj);
    }
    public God(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        Initialize(obj);
    }

    void Initialize(TiledObject obj)
    {
        SetOrigin(width / 2, height / 2);
        nextLevel = obj.GetStringProperty("nextLevel", "StartScene");
    }

    void Update()
    {
        Position = new Vec2(x,y);
        CheckBallCollision();
        
    }

    void CheckBallCollision()
    {
        //Finds all  the ball objects in the scene
        BallObjects = game.FindObjectsOfType<Ball>().ToList();
        for (int i = 0; i < BallObjects.Count; i++)
        {
            if (BallObjects[i].tester) return;
                //Get the distance between the ball and the god. In absolute so it is always a positive number which means less methods
                float distX = Mathf.Abs(Position.x - BallObjects[i].Position.x);
            float distY = Mathf.Abs(Position.y - BallObjects[i].Position.y);

            //checks when that distance is less than the radius of the ball and the size of the god
            if (distX < BallObjects[i].radius + width / 2 && distY < BallObjects[i].radius + width / 2)
            {
                Console.WriteLine("ENTER"); //put a method here to change the scene
                Console.WriteLine("Stars given: {0}", ((MyGame)game).deathCount);
                ((MyGame)game).PassLevel();
            }
        }
    }
}
