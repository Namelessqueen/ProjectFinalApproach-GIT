using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;


internal class Button : AnimationSprite
{
    string action;
    string CurrentScene;
    Vec2 Position;
    public Button(string imageFile = "Square.png", TiledObject obj = null) : base(imageFile,1, 1, -1)
    {
        Initialize(obj);
    }
    public Button(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        Initialize(obj);
    }

    void Initialize(TiledObject obj)
    {
        SetOrigin(width / 2, height / 2);
        action = obj.GetStringProperty("action", "Start");
        CurrentScene = obj.GetStringProperty("CurrentScene", "Level_1");//gets the strring named action from tiled
    }

    void Update()
    {
        Position = new Vec2(x,y);
        ButtonCollision();
        
    }

    void ButtonCollision() // Checks for collision with the mouse inside the button sprite
    {
        float distX = Mathf.Abs(Position.x - Input.mouseX);
        float distY = Mathf.Abs(Position.y - Input.mouseY);

        
        if (distX < width / 2 && distY < height / 2 && ( Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)))
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
            }
            if (action == "Retry")
            {
                ((MyGame)game).LoadScene(CurrentScene+".tmx");
            }
        }
    }
}

