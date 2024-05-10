using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

internal class Ball : AnimationSprite
{
    public Vec2 Position;
    public Vec2 Velocity;
    public int radius;
    Vec2 PlayerPos;
    float Angle;


    public Ball(float pAngle, Vec2 pPlayerPos) : base("Ball.png", 1, 1, -1, false, false)
    {
        Angle = pAngle;
        PlayerPos = pPlayerPos;
        InitializeSheep();

    }

    public Ball(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        Initialize();
    }

    void Initialize()
    {
        radius = 16;
        SetOrigin(width / 2, height / 2);
        SetScaleXY(radius * 2, radius * 2);

        Position = new Vec2(100, game.height / 2);
        Velocity = new Vec2(4, 0);
    }

    void InitializeSheep()
    {
        SetOrigin(width / 2, height / 2);
        SetColor(255,0,0);

        Position = PlayerPos;
        Velocity = new Vec2(0, -1);
        Velocity.SetAngleDegrees(Angle);
    }
    void BoundaryWrap()
    {
        if (Position.x - radius > game.width)
        {
            Position.x = 0;
        }

        if (Position.y - radius > game.height)
        {
            Position.y = 0;
        }

        if (Position.x + radius < 0)
        {
            Position.x = game.width;
        }

        if (Position.y + radius < 0)
        {
            Position.y = game.height;
        }
        Console.WriteLine(Position);
    }

    void Update()
    {
        x = Position.x;
        y = Position.y;
        Position += Velocity;

        BoundaryWrap();
    }
}