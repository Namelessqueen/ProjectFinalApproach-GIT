using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;


internal class HUD : GameObject 
{
    EasyDraw levelFail;

    public HUD()
    {
        levelFail = new EasyDraw(game.width, game.height, false); 
    }

}

