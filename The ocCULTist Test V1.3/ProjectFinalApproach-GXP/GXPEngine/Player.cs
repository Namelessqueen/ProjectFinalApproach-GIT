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
    Sound cannonTurn = new Sound("Barrel_Turn.mp3");
    Sound scream = new Sound("scream.mp3");
    Sound rat = new Sound("rat.mp3");
    Sound launch = new Sound("launch.mp3");

    public Player(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        target = new Vec2(0, -1);
        SetCycle(0, 1);

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
        ShootNormal();
        Animate(0.03f * Time.deltaTime);
    }

    void Rotation()
    {
        if (!((MyGame)game).paused && !((MyGame)game).success && ((MyGame)game).deathCount != 0)
        {
            if (Input.GetMouseButtonDown(0)) target = new Vec2(Input.mouseX - playerPos.x, Input.mouseY - playerPos.y);
           

             if (Input.GetMouseButtonDown(0))
            {
               cannonTurn.Play().Volume = 0.1f;
            }

            float targetAngle = Mathf.Clamp(target.GetAngleDegrees(), -135, -45);

            if (targetAngle > rotation + 0.5f)
            {   
                rotation += 0.07f * Time.deltaTime;
            }
            else if (targetAngle < rotation - 0.5f)
            {
                rotation -= 0.07f * Time.deltaTime;
            }
        }
    }

    List<Reload> goats;

    int cannonTimer = 0;
    bool normalShoot = false;
    void BallSpawn()
    {
        if (!((MyGame)game).paused && !((MyGame)game).success && ((MyGame)game).deathCount != 0)
        {
            //Add balls the the game by pressing left mouse (normal ball) and T (test ball)
            if (Input.GetKeyDown(Key.SPACE) && goats.Count != 0 && ((MyGame)game).success == false)
            {
                normalShoot = true; // Not correctly added! needs fixing
            }
            if (Input.GetKeyDown(Key.T))
            {
                rat.Play().Volume = 0.1f;
                game.AddChild(new Ball("spr_rat_01.png", 1, 1, rotation, playerPos, true));    // Not correctly added! needs fixing
            }
        }
    }

    void ShootNormal()
    {
        if (normalShoot)
        {
            if (cannonTimer > 0 && cannonTimer < 18)
            {
                launch.Play().Volume = 0.15f;
            }
            cannonTimer += Time.deltaTime;
            SetCycle(0, 10);
            if (cannonTimer > 280)
            {
                scream.Play().Volume = 0.2f;
                game.AddChild(new Ball(goatType, 7, 4, rotation, playerPos));
                SetCycle(0, 1);
                cannonTimer = 0;
                normalShoot = false;
            }
        }
    }

    void Reloader()
    {
        if (!((MyGame)game).paused && !((MyGame)game).success && ((MyGame)game).deathCount != 0)
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

}

