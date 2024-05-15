using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;


internal class Button : AnimationSprite
{
    public string action;
    string CurrentScene;
    Vec2 Position;
    public Button(string imageFile = "Square.png") : base(imageFile, 2, 1, -1)
    {
        Initialize();
    }
    public Button(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        Initialize(obj);
    }

    void Initialize(TiledObject obj)
    {
        SetOrigin(width / 2, height / 2);
        action = obj.GetStringProperty("action", "Start");
        CurrentScene = obj.GetStringProperty("CurrentScene", "Level_1");//gets the string named action from tiled

        if (action == "FailRetry")
        {
            alpha = 0;
        }
    }

    void Initialize()
    {
        SetOrigin(width / 2, height / 2);
        CurrentScene = "Level_1";
    }

    void Update()
    {
        Position = new Vec2(x, y);
        ButtonCollision();
        FailButtons();

        if (((MyGame)game).deathCount == 0 && action != "FailRetry" && action != "Menu" && action != "Continue" || ((MyGame)game).success == true && action != "FailRetry" && action != "Menu" && action != "Continue") 
        {
            Destroy();
        }

    }

    void FailButtons()
    {
        if (action == "FailRetry" || action == "Menu")
        {
            if (((MyGame)game).deathCount == 0 || ((MyGame)game).success)
            {
                alpha = 1;
            } else
            {
                alpha = 0;
            }
        }

        if (action == "Continue")
        {
            if (((MyGame)game).success)
            {
                alpha = 1;
            }
            else
            {
                alpha = 0;
            }
        }       
    }
    


    void ButtonCollision() // Checks for collision with the mouse inside the button sprite
    {
        float distX = Mathf.Abs(Position.x - Input.mouseX);
        float distY = Mathf.Abs(Position.y - Input.mouseY);


        if (distX < width / 2 && distY < height / 2 && (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)))
        {
            ActivateButton();
            SetFrame(1); //animates the sprite when you mouse hovers over it
        }
        else SetFrame(0);
    }

    void ActivateButton() //Checks if the left mouse button is clicked and than preforms an action. here you can add actions when needed for different buttons
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (action == "Start")
            {
                ((MyGame)game).LoadScene("Level_1.tmx");
                ((MyGame)game).currentLevel = 1;
                CurrentScene = "Level_1";
            }

            if (action == "Retry")
            {
                ((MyGame)game).LoadScene(CurrentScene + ".tmx");
            }

            if (action == "FailRetry" && ((MyGame)game).deathCount == 0 || action == "FailRetry" && ((MyGame)game).success)
            {
                ((MyGame)game).success = false;
                ((MyGame)game).LoadScene("Level_" + ((MyGame)game).currentLevel + ".tmx");
            }

            if (action == "Menu" && ((MyGame)game).deathCount == 0 || action == "Menu" && ((MyGame)game).success)
            {
                ((MyGame)game).success = false;
                ((MyGame)game).LoadScene("StartScene.tmx");
            }

            if (action == "Continue" && ((MyGame)game).success)
            {
                ((MyGame)game).success = false;
                ((MyGame)game).currentLevel++;
                ((MyGame)game).LoadScene("Level_" + ((MyGame)game).currentLevel + ".tmx");
            }
        }


    }
}

