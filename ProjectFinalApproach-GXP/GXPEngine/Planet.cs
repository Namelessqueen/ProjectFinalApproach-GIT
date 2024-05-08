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
        Position = new Vec2(game.width/2 + 5, game.height/2 + 5);
        radius = 32;
    }

    void Update()
    {
        x = Position.x;
        y = Position.y;
    }
}

