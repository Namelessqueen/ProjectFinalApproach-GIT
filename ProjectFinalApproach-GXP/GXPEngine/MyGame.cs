using System;                                   
using GXPEngine;                                
using System.Drawing;
using System.Collections.Generic;
using System.Web;

public class MyGame : Game {

	string nextScene = null;

	public MyGame() : base(800, 608, false)    
	{
	    OnAfterStep += CheckLoadScene;
		LoadScene("ballTest.tmx");
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

	static void Main()                          
	{
		new MyGame().Start();                  
	}
}