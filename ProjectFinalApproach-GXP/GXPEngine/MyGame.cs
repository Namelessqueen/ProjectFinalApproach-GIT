using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {
	public MyGame() : base(800, 600, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		// Draw some things on a canvas:
		EasyDraw canvas = new EasyDraw(800, 600);
		canvas.Clear(Color.Aqua);
		canvas.Fill(Color.Yellow);
		canvas.Ellipse(game.width / 2, game.height / 2, 100, 100);
		canvas.Fill(50);
		canvas.TextSize(20);
		canvas.TextAlign(CenterMode.Center, CenterMode.Center);
		canvas.Text("Planet", width / 2, height / 2);

		// Add the canvas to the engine to display it:
		AddChild(canvas);
	}

	// For every game object, Update is called every frame, by the engine:
	void Update() {
	
	}

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                  
	}
}