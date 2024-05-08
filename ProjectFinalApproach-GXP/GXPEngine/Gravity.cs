﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;
using GXPEngine;
using System.Runtime.CompilerServices;


internal class Gravity : AnimationSprite
{
    public Vec2 Position;
    public Vec2 Velocity;
    int radius;
    float gravityStrength = 0.04f;

    public Gravity() : base("Force.png", 1, 1, -1, false, false)
    {
        Initialize();
    }

    public Gravity(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        Initialize();
    }

    void Initialize()
    {
        radius = 125;
        SetOrigin(width / 2, height / 2);
        SetScaleXY(radius * 2, radius * 2);

        Position = new Vec2(game.width / 2, game.height / 2);
    }

    Vec2 ballDistance;
    Ball ball;

    void CheckOverlap()
    {

        ball = game.FindObjectOfType<Ball>();


        if (ball != null)
        {
            ballDistance = Position - ball.Position;

            if (ballDistance.Length() <= radius + ball.radius)
            {
                AlterCourse();
            }
            else
            {
                ball.Velocity = ball.Velocity.Normalized() * ball.speed;
            }
        }
    }

    bool moveLeft = false;
    bool moveRight = false;
    bool moveUp = false;
    bool moveDown = false;

    void AlterCourse()
    {
        
        if (ball.Position.x >= Position.x)
        {
            moveLeft = true;
        } else
        {
            moveLeft = false;
        }

        if (ball.Position.x < Position.x)
        {
            moveRight = true;
        } else
        {
            moveRight = false;
        }

        if (ball.Position.y < Position.y)
        {
            moveDown = true;
        }
        else
        {
            moveDown = false;
        }

        if (ball.Position.y >= Position.y)
        {
            moveUp = true;
        }
        else
        {
            moveUp = false;
        }

        if (moveLeft)
        {
            ball.Velocity.x -= gravityStrength * ball.Velocity.Length();

        }

        if (moveRight)
        {
            ball.Velocity.x += gravityStrength * ball.Velocity.Length(); 
           
        }

        if (moveUp)
        {
            ball.Velocity.y -= gravityStrength * ball.Velocity.Length();
        }

        if (moveDown)
        {
            ball.Velocity.y += gravityStrength * ball.Velocity.Length();
        }

        ball.Velocity = ball.Velocity.Normalized() * ball.speed;
    }



    void Update()
    {
        x = Position.x;
        y = Position.y;

        CheckOverlap();
    }
}

