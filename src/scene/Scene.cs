using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;
public abstract class Scene
{
    public string Name { get; protected set; }

    public Camera Camera { get; private set; } = new Camera();

    // All GameObjects in the scene
    public List<GameObject> GameObjects { get; private set; } = new List<GameObject>();

    public bool Paused { get; private set; } = false;

    public Scene(string name)
    {
        Name = name;
    }

    // Called when setting the scene as the current scene
    public abstract void LoadContent();

    // Called when setting the scene as the current scene
    public abstract void Begin();

    // Called when setting a separate scene as the current scene
    public abstract void End();

    public virtual void Draw()
    {
        SerpentEngine.Draw.SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, Camera.Matrix);

        foreach (GameObject gameObject in GameObjects)
        {
            gameObject.Draw();
        }

        SerpentEngine.Draw.SpriteBatch.End();
    }

    public virtual void Update()
    {
        Camera.Update();

        foreach (GameObject gameObject in GameObjects)
        {
            gameObject.Update();
        }
    }

    public T CreateAndAddGameObject<T>() where T : GameObject, new()
    {
        T gameObject = new T();
        AddGameObject(gameObject);

        return gameObject;
    }

    public void AddGameObject(GameObject gameObject)
    {
        GameObjects.Add(gameObject);
        gameObject.Load();
    }

    public void Remove(GameObject gameObject)
    {
        GameObjects.Remove(gameObject);
    }

    public void Pause()
    {
        Paused = true;
    }
}
