using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;


internal class Reload : AnimationSprite
{

    public Reload(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        Initialize();
        _cols = cols;
    }

    void Initialize()
    {
        Randomize();
    }

    void Update()
    {
        Animate();
    }

    public int pickedSprite;

    void Randomize()
    {
        //pick a random sprite out of 4 to display as the emblem
        pickedSprite = Utils.Random(0, _cols);
        SetCycle(pickedSprite);
        Console.WriteLine("Frame picked: {0}", pickedSprite);
       
    }
}

