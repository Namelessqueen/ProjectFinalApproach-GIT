using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;


internal class Player : AnimationSprite
{
    Vec2 mousePos, playerPos, mouseAnlge;
    EasyDraw _easyDraw;
    HUD hud = null;

    public Player(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        
    }

    void Update()
    {
        SetOrigin(20, height/2);
        playerPos.x = x;
        playerPos.y = y;

        Rotation();

        if (hud == null)
        {
            hud = game.FindObjectOfType<HUD>();
        }

        Reloader();
        BallSpawn();
    }

    void Rotation()
    {
        float dx = Input.mouseX - playerPos.x;
        float dy = Input.mouseY - playerPos.y;

        // Get angle to mouse, convert from radians to degrees:
        float targetAngle = Mathf.Atan2(dy, dx) * 180 / Mathf.PI;
        if (targetAngle > rotation + 0.5f)
        {
            rotation += 0.5f;
        }
        else if (targetAngle < rotation - 0.5f)
        {
            rotation -= 0.5f;
        }
        //rotation = 0;
        //rotation = Mathf.Clamp(rotation -= 0.5f,-45,-115);
        Console.WriteLine(rotation);

        mousePos = new Vec2(Input.mouseX, Input.mouseY);
        mouseAnlge = playerPos - mousePos;
        //drawing aiming line(eventhough it is transparant)
    }

    List<Reload> goats;
    void BallSpawn()
    {
        //Add balls the the game by pressing left mouse (normal ball) and T (test ball)
        if (Input.GetMouseButtonDown(0) && goats.Count != 0)
        {   
            game.AddChild(new Ball(mouseAnlge.GetAngleDegrees(), playerPos));    // Not correctly added! needs fixing
        }
        if (Input.GetKeyDown(Key.T))
        {
            game.AddChild(new Ball(mouseAnlge.GetAngleDegrees(), playerPos, true));    // Not correctly added! needs fixing
        }
    }

    void Reloader()
    {
       goats = game.FindObjectsOfType<Reload>().ToList();

        for(int i = 0; i < goats.Count; i++)
        {
            //if there are attempts left...
            if(goats.Count != 0)
            {
               //destroy the most left emblem (also depleting one attempt)
               if(Input.GetMouseButtonDown(0))
                {
                    goats[0].Destroy();
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (goats.Count != 0)
            {
                Console.WriteLine("Type of goat: {0}", goats[0].pickedSprite);
            }
        }
    }

}

