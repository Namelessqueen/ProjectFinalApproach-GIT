using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

    internal class Ball : AnimationSprite
    {
        public Vec2 Position, Velocity;

    /*public Ball() : base("Ball.png")
    {
        SetOrigin(width/2, height/2);
        Posision = new Vec2(game.width, game.height/2);

    }*/

    public Ball(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
            SetOrigin(width / 2, height / 2);
            Position = new Vec2(game.width, game.height / 2);
        }

        void Update()
        {
        Position += Velocity;
        }
    }