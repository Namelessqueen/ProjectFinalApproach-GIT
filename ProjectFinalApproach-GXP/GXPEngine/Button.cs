using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;


internal class Button : AnimationSprite
{
    public Button() : base("Square.png", 1, 1, -1)
    {

    }
    public Button(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(Key.SPACE))
            ((MyGame)game).LoadScene("Level_1.tmx");
    }
}

