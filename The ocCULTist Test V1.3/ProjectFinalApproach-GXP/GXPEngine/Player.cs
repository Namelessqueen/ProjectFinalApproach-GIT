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
    Vec2 playerPos;
    HUD hud = null;
    Vec2 target = new Vec2(0, -1);
    string goatType;

    public Player(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        target = new Vec2(0, -1);
    }

    void Update()
    {
        playerPos.x = x;
        playerPos.y = y;
        SetOrigin(20, height / 2);

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
        if (Input.GetMouseButtonDown(0)) target = new Vec2(Input.mouseX - playerPos.x, Input.mouseY - playerPos.y);
        float targetAngle = Mathf.Clamp(target.GetAngleDegrees(), -135, -45);

        if (targetAngle > rotation + 0.5f)
        {
            rotation += 0.5f;
        }
        else if (targetAngle < rotation - 0.5f)
        {
            rotation -= 0.5f;
        }
    }

    List<Reload> goats;
    void BallSpawn()
    {
        //Add balls the the game by pressing left mouse (normal ball) and T (test ball)
        if (Input.GetKeyDown(Key.SPACE) && goats.Count != 0 && ((MyGame)game).success == false)
        {
            game.AddChild(new Ball(goatType, 7, 4, rotation, playerPos));    // Not correctly added! needs fixing
        }
        if (Input.GetKeyDown(Key.T))
        {
            game.AddChild(new Ball("spr_rat_01.png", 1, 1, rotation, playerPos, true));    // Not correctly added! needs fixing
        }
    }

    void Reloader()
    {
        goats = game.FindObjectsOfType<Reload>().ToList();

        for (int i = 0; i < goats.Count; i++)
        {
            //if there are attempts left...
            if (goats.Count != 0)
            {
                //destroy the most left emblem (also depleting one attempt)
                if (Input.GetKeyDown(Key.SPACE))
                {
                    goats[0].Destroy();
                }
            }
        }

        if (Input.GetKeyDown(Key.SPACE))
        {
            if (goats.Count != 0)
            {
                if (goats[0].pickedSprite == 0)
                {
                    goatType = "spr_goat1.png";
                }
                if (goats[0].pickedSprite == 1)
                {
                    goatType = "spr_goat2.png";
                }
                if (goats[0].pickedSprite == 2)
                {
                    goatType = "spr_goat3.png";
                }
                Console.WriteLine("Type of goat: {0}", goats[0].pickedSprite);
            }
        }

    }

}

