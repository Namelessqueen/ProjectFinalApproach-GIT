using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;


internal class HUD : GameObject 
{
    EasyDraw levelFail;
    EasyDraw levelNumber;
    Button retry;
    Button menu;
    public static Font gameFont = new Font("Calamity Regular", 200);
 
    public HUD()
    {
        levelFail = new EasyDraw("level_failed.png", false);
        levelFail.width = game.width;
        levelFail.height = game.height;
        levelFail.alpha = 0;
        AddChild(levelFail);

        levelNumber = new EasyDraw(100, 100, false);
        levelNumber.TextFont(gameFont);
        levelNumber.SetXY(game.width/2 + 50, game.height/2 - 81);
        levelNumber.TextSize(21);   
        AddChild(levelNumber);

        retry = new Button("button_retry.png");
        retry.SetXY(game.width / 2, game.height / 2 + 130);
        retry.action = "FailRetry";
        retry.alpha = 0;
        retry.SetScaleXY(1.5f);
        AddChild(retry);

        menu = new Button("button_menu.png");
        menu.SetXY(game.width / 2,game.height - 150);
        menu.action = "Menu";
        menu.alpha = 0;
        AddChild(menu);

    }

    int failTimer = 0;

    void LevelFailed()
    {     
        if (((MyGame)game).deathCount == 0)
        {
            failTimer++;
            levelFail.alpha = 1;
            levelNumber.alpha = 1;
        }   
        else
        {
            failTimer = 0;
            levelFail.alpha = 0;
            levelNumber.alpha = 0;
        }

        if (((MyGame)game).currentLevel == 1)
        {
            levelNumber.Text("1");
        }
    }

    void Update()
    {
        LevelFailed();
    }

}

