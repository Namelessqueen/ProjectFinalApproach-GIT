using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;


internal class Planet : AnimationSprite
{
    public Vec2 Position;
    public int radius;
    List<Gravity> gravityObjects;
    public Planet() : base("Planet.png", 1, 1, -1, false, false)
    {
        Initialize();
    }

    public Planet(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        Initialize();
    }

    void Initialize()
    {
        SetOrigin(width/2, height/2);
       
        radius = 32;
       
    }

    void Update()
    {
        //first make sure that the start position is the same as the one in Tiled, since the Initialize void is not updated before
        Position = new Vec2(x, y);
        PlanetAlignment();
        Animation();

        x = Position.x;
        y = Position.y;
        
    }

    void PlanetAlignment()
    {
        //search for all the gravity objects
        gravityObjects = game.FindObjectsOfType<Gravity>().ToList();

        for (int i = 0; i < gravityObjects.Count; i++)
        {
            
            Vec2 dist = gravityObjects[i].Position - Position;

            //this selects the closest gravity object to allign itself with
            if (dist.Length() < 125) //125 is gravity radius
            {
                //makes planet always align with the gravity object
                Position = gravityObjects[i].Position;
            }
        }
    }

    void Animation()
    {
        //Animate the planets with different ammout of frames
        SetCycle(0, _cols * _rows, 4);
        Animate();
    }
}

