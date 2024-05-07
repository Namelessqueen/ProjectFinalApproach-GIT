using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;



class Scene : GameObject
{
    TiledLoader loader;

    public Scene(string filename)
    {
        Console.WriteLine("Creating new scene");
        loader = new TiledLoader(filename);
        CreateScene();

    }

    public void CreateScene(bool includeImageLayers = true)
    {
        Console.WriteLine("Building scene");
        loader.addColliders = false;
        loader.rootObject = game;
        if (includeImageLayers)
        {
            loader.LoadImageLayers();
        }
        loader.autoInstance = true;
        loader.rootObject = this;
        loader.addColliders = true;
        loader.LoadTileLayers();
        loader.LoadObjectGroups();
    }
}

