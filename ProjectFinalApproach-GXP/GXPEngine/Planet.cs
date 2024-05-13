﻿using System;
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
        Position = new Vec2(x, y);
        PlanetAlingment();
        Animation();

        x = Position.x;
        y = Position.y;
        
    }

    void PlanetAlingment()
    {
        gravityObjects = game.FindObjectsOfType<Gravity>().ToList();

        for (int i = 0; i < gravityObjects.Count; i++)
        {
            Vec2 dist = gravityObjects[i].Position - Position;

            if (dist.Length() < 125)
            {
                Position = gravityObjects[i].Position;
            }
        }
    }

    void Animation()
    {
        SetCycle(0, 21, 4);
        Animate();
    }
}

