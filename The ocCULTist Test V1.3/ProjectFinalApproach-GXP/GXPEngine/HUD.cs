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
    EasyDraw levelPassed;
    EasyDraw levelNumber;
    EasyDraw stars;
    Button _continue;
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

        levelPassed = new EasyDraw("level_passed.png", false);
        levelPassed.width = game.width;
        levelPassed.height = game.height;
        levelPassed.alpha = 0;
        AddChild(levelPassed);

        levelNumber = new EasyDraw(100, 100, false);
        levelNumber.TextFont(gameFont);
        levelNumber.TextSize(21);
        AddChild(levelNumber);

        stars = new EasyDraw(100, 100, false);
        stars.TextFont(gameFont);
        stars.TextSize(40);
        stars.TextAlign(CenterMode.Center, CenterMode.Center);
        stars.SetXY(game.width / 2 - 55, game.height / 2 + 75);
        stars.alpha = 0;
        AddChild(stars);


        retry = new Button("button_retry.png");
        retry.action = "FailRetry";
        retry.alpha = 0;
        AddChild(retry);

        menu = new Button("button_menu.png");
        menu.action = "Menu";
        menu.alpha = 0;
        AddChild(menu);

        _continue = new Button("button_continue.png");
        _continue.action = "Continue";
        _continue.SetXY(game.width/2 + 100, game.height - 235);
        _continue.alpha = 0;
        AddChild(_continue);
    }


    void LevelPassed()
    {
        if (((MyGame)game).success == true)
        {
            levelNumber.SetXY(game.width / 2 + 45, game.height / 2 - 94);
            levelPassed.alpha = 1;
            levelNumber.alpha = 1;
            stars.alpha = 1;

            retry.width = 70;
            retry.height = 70;
            retry.SetXY(game.width / 2 - 150, game.height - 235);

            menu.SetXY(game.width / 2 - 50, game.height - 235);
            menu.width = 70;
            menu.height = 70;

            stars.ClearTransparent();
            stars.Text(((MyGame)game).deathCount.ToString());
        }
        else
        {
            levelPassed.alpha = 0;
            stars.alpha = 0;
            if (((MyGame)game).deathCount != 0)
            {
                levelNumber.alpha = 0;
            }
        }
    }

    void LevelFailed()
    {
        if (((MyGame)game).deathCount == 0)
        {
            levelNumber.SetXY(game.width / 2 + 50, game.height / 2 - 81);
            levelFail.alpha = 1;
            levelNumber.alpha = 1;

            retry.SetScaleXY(1.5f);
            retry.SetXY(game.width / 2, game.height / 2 + 130);

            menu.SetXY(game.width / 2, game.height - 150);
            menu.SetScaleXY(1);

        }
        else
        {
            levelFail.alpha = 0;
            levelNumber.alpha = 0;
        }

        if (((MyGame)game).currentLevel == 1)
        {
            LevelNumber(1);
        }

        if (((MyGame)game).currentLevel == 2)
        {
            LevelNumber(2);
        }


    }

    void LevelNumber(int number)
    {
        levelNumber.Text(number.ToString());
    }

    void Update()
    {
        LevelFailed();
        LevelPassed();

        
    }

}

