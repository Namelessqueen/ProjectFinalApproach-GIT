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
    EasyDraw starOne;
    EasyDraw starTwo;
    EasyDraw starThree;
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

        starOne = new EasyDraw("star_goat1.png", false);
        starOne.SetXY(game.width / 2 - 140, game.height / 2 + 85);
        starOne.alpha = 0;
        AddChild(starOne);

        starTwo = new EasyDraw("star_goat2.png", false);
        starTwo.SetXY(game.width / 2 - 40, game.height / 2 + 85);
        starTwo.alpha = 0;
        AddChild(starTwo);

        starThree = new EasyDraw("star_goat3.png", false);
        starThree.SetXY(game.width / 2 + 60, game.height / 2 + 85);
        starThree.alpha = 0;
        AddChild(starThree);



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
            starOne.alpha = 1;

            if (((MyGame)game).deathCount >= 2)
            {
                starTwo.alpha = 1;
            }

            if (((MyGame)game).deathCount == 3)
            {
                starThree.alpha = 1;
            }

            retry.width = 70;
            retry.height = 70;
            retry.SetXY(game.width / 2 - 150, game.height - 235);

            menu.SetXY(game.width / 2 - 50, game.height - 235);
            menu.width = 70;
            menu.height = 70;
        }
        else
        {
            levelPassed.alpha = 0;
            starOne.alpha = 0;
            starTwo.alpha = 0;
            starThree.alpha = 0;

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

