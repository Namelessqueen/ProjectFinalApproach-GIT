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
        playerPos = new Vec2(x,y);
        _easyDraw = new EasyDraw(game.width, game.height, false);
        _easyDraw.SetOrigin(0, 0);
      
        AddChild(_easyDraw);
    }

    void Update()
    {
        playerPos.x = x;
        playerPos.y = y;
        _easyDraw.SetXY(-playerPos.x, -playerPos.y);

        _easyDraw.ClearTransparent();
        //_easyDraw.Clear(255,0,0); _easyDraw.alpha = 0.5f;

        mousePos = new Vec2(Input.mouseX, Input.mouseY);
        mouseAnlge = playerPos - mousePos;
        _easyDraw.Stroke(255); _easyDraw.StrokeWeight(3);
        _easyDraw.Line(x, y, mousePos.x, mousePos.y);
        BallSpawn();

    }

    void BallSpawn()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            game.AddChild(new Ball(mouseAnlge.GetAngleDegrees(), playerPos));    // Not correctly added! needs fixing
        }
    }

}

