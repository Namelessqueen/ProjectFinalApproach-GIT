using System;                                   
using GXPEngine;                                
using System.Drawing;
using System.Collections.Generic;
using System.Web;

public class MyGame : Game {

	string nextScene = null;

	//how many attempts the player gets:
	public int deathCount = 3;
	public int currentLevel = 0;
	public bool success = false;
	int stars = 0;

	public MyGame() : base(768, 1024, false)    
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
            AddChild(new HUD());
            deathCount = 3;
			nextScene = null;
		}
	}

	public void PassLevel()
	{
		success = true;
	}


	void Stars()
	{

	}

	void Update()
	{
		
	}

	static void Main()                          
	{
		new MyGame().Start();                  
	}
}