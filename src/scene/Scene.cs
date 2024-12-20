﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SerpentEngine;
public abstract class Scene
{
    public string Name { get; protected set; }

    public Camera Camera { get; private set; } = new Camera();

    public List<GameObject> UIElements { get; private set; } = new List<GameObject>();

    // All GameObjects in the scene
    private List<GameObject> GameObjects = new List<GameObject>();

    private RenderTarget2D uiRenderTarget = new RenderTarget2D(SerpentGame.Instance.GraphicsDevice, GraphicsConfig.SCREEN_WIDTH, GraphicsConfig.SCREEN_HEIGHT);

    public bool Paused { get; private set; } = false;

    private GameObject[] gameObjectsSnapshot = new GameObject[0];

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
        SerpentGame.Instance.GraphicsDevice.SetRenderTarget(uiRenderTarget);

        SerpentGame.Instance.GraphicsDevice.Clear(Color.Transparent);

        // Draw UI game objects
        SerpentEngine.Draw.SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, Matrix.Identity * Matrix.CreateScale(Camera.UIScale));

        foreach (GameObject uiElement in UIElements)
        {
            uiElement.Draw();
        }

        SerpentEngine.Draw.SpriteBatch.End();

        SerpentGame.Instance.GraphicsDevice.SetRenderTarget(null);

        SerpentGame.Instance.GraphicsDevice.Clear(GraphicsConfig.BACKGROUND_COLOR);

        // Draw scene game objects
        SerpentEngine.Draw.SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, Camera.Matrix);

        foreach (GameObject gameObject in gameObjectsSnapshot)
        {
            gameObject.Draw();
        }

        SerpentEngine.Draw.SpriteBatch.End();

        // Draw UI render target

        SerpentEngine.Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
        SerpentEngine.Draw.SpriteBatch.Draw(uiRenderTarget, Vector2.Zero, Color.White);
        SerpentEngine.Draw.SpriteBatch.End();
    }

    public virtual void Update()
    {
        Camera.Update();

        lock (GameObjects)
        {
            gameObjectsSnapshot = GameObjects.ToArray();
        }

        foreach (GameObject ui in UIElements.ToList())
        {
            ui.Update();
        }

        foreach (GameObject gameObject in gameObjectsSnapshot)
        {
            gameObject.Update();
        }
    }

    public List<GameObject> GetGameObjects()
    {
        List<GameObject> foundGameObjects = new List<GameObject>();

        foreach (GameObject gameObject in GameObjects.ToArray())
        {
            foundGameObjects.Add(gameObject);
        }

        return foundGameObjects;
    }

    public List<GameObject> GetGameObjects<T>() where T : GameObject
    {
        List<GameObject> foundGameObjects = new List<GameObject>();

        foreach (GameObject gameObject in GameObjects)
        {
            if (gameObject is T)
            {
                foundGameObjects.Add(gameObject);
            }
        }

        return foundGameObjects;
    }

    public T GetGameObject<T>() where T : GameObject
    {
        foreach (GameObject gameObject in GameObjects)
        {
            if (gameObject is T)
            {
                return (T)gameObject;
            }
        }
        return null;
    }

    public GameObject GetGameObjectAt(Vector2 position)
    {
        foreach (GameObject gameObject in GetGameObjects())
        {
            if (gameObject.Position == position)
            {
                return gameObject;
            }
        }

        return null;
    }

    public T GetGameObjectAt<T>(Vector2 position) where T : GameObject
    {
        foreach (GameObject gameObject in GetGameObjects())
        {
            if (gameObject is T && gameObject.Position == position)
            {
                return (T)gameObject;
            }
        }

        return null;
    }

    public List<GameObject> GetGameObjectsAt(Vector2 position)
    {
        List<GameObject> gameObjects = new List<GameObject>();

        foreach (GameObject gameObject in GetGameObjects())
        {
            if (gameObject.Position == position)
            {
                gameObjects.Add(gameObject);
            }
        }

        if (gameObjects.Count == 0) return null;

        return gameObjects;
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
        gameObject.Initialize();
    }

    public void AddUIElement(GameObject uiElement)
    {
        UIElements.Add(uiElement);
        uiElement.Initialize();
    }

    public T GetUIElement<T>() where T : GameObject
    {
        foreach (GameObject uiElement in UIElements)
        {
            if (uiElement is T)
            {
                return (T)uiElement;
            }
        }
        return null;
    }

    public GameObject GetUIElementAt(Vector2 position)
    {
        foreach (GameObject uiElement in UIElements)
        {
            Rectangle bounds = new Rectangle((int)uiElement.Position.X - (int)(uiElement.Size.X / 2), (int)uiElement.Position.Y - (int)(uiElement.Size.Y / 2), (int)uiElement.Size.X, (int)uiElement.Size.Y);

            if (!uiElement.Enabled) continue;

            if (bounds.Contains(position))
            {
                return uiElement;
            }
        }

        return null;
    }

    public void AddUIElement(UiElementGroup group)
    {

        UIElements.Add(group.Parent);
        group.Parent.Initialize();

        foreach (KeyValuePair<GameObject, Vector2> uiEntry in group.Children)
        {
            GameObject ui = uiEntry.Key;
            Vector2 offset = uiEntry.Value;

            UIElements.Add(ui);
            ui.Initialize();
        }
    }

    public void Remove(GameObject gameObject)
    {
        if (UIElements.Contains(gameObject))
        {
            UIElements.Remove(gameObject);
        }
        else
        {
            GameObjects.Remove(gameObject);
        }
    }

    public virtual void Pause()
    {
        Paused = true;
    }

    public virtual void Resume()
    {
        Paused = false;
    }
}
