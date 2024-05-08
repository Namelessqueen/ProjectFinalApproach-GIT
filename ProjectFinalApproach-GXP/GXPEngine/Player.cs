using System;
using System.Collections.Generic;
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

    public Player(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        SetOrigin(width/2,height/2);
        SetXY(300,300);
        _easyDraw = new EasyDraw(game.width*2 + width, game.height*2 + height, false);
        playerPos = new Vec2(this.x,this.y);
        _easyDraw.SetOrigin(_easyDraw.width / 2, _easyDraw.height / 2);
        _easyDraw.SetXY(playerPos.x, playerPos.y);
        AddChild(_easyDraw);
    }

    void Update()
    {
        x = Position.x;
        y = Position.y;

        _easyDraw.ClearTransparent();
        mousePos = new Vec2(Input.mouseX, Input.mouseY);
        mouseAnlge = mousePos - playerPos;
        _easyDraw.Stroke(255); _easyDraw.StrokeWeight(3);
        _easyDraw.Line(_easyDraw.width/2, _easyDraw.height/2,Input.mouseX, Input.mouseY);
        BallSpawn();
        //Console.WriteLine("the mousePos is {0}, the position of the player is {1}, {2}", mousePos, x,y);
        Console.WriteLine(playerPos);

    }

    void BallSpawn()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            //game.AddChild(new Ball(mousePos));    // Not correctly added! needs fixing
        }
    }

}

