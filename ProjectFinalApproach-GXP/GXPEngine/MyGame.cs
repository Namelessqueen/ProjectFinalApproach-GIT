using System;                                   
using GXPEngine;                                
using System.Drawing;
using System.Collections.Generic;
using System.Web;

public class MyGame : Game {

	string nextScene = null;

	//how many attempts the player gets:
	public int deathCount = 3;

	public MyGame() : base(1024, 1024, false)    
	{
	    OnAfterStep += CheckLoadScene;
		LoadScene("StartScene.tmx");
	}
	void DestroyAll()
	{
		List<GameObject> children = GetChildren();
		foreach (GameObject child in children)
		{
			child.Destroy();
		}
	}

	public void LoadScene(string filename)
	{
		nextScene = filename;
	}

	void CheckLoadScene()
	{
		if (nextScene != null)
		{
			DestroyAll();
			AddChild(new Scene(nextScene));

			nextScene = null;
		}
	}

	void Update()
	{
		if (deathCount == 0)
		{
			LoadScene("Level_1.tmx");
			deathCount = 3;
		}
	}

	static void Main()                          
	{
		new MyGame().Start();                  
	}
}