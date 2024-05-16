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
	public bool paused = false;
	Sound music = new Sound("music.mp3");

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

	int time = 0;
	bool musicPlaying = false;

	void PlayMusic()
	{
		time += Time.deltaTime;

		if (deathCount != 0 && !musicPlaying)
		{
            musicPlaying = true;
            music.Play().Volume = 0.1f;
		} 

		if (time/1000 > 33)
		{
			musicPlaying = false;
			time = 0;
		}
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

	void Update()
	{
		PlayMusic();
	}

	static void Main()                          
	{
		new MyGame().Start();                  
	}
}