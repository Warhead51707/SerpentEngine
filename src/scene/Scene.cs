using Microsoft.Xna.Framework;
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

        SerpentGame.Instance.GraphicsDevice.Clear(Color.Black);

        // Draw scene game objects
        SerpentEngine.Draw.SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, Camera.Matrix);

        foreach (GameObject gameObject in GameObjects)
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
        foreach (GameObject ui in UIElements.ToList())
        {
            ui.Update();
        }
        foreach (GameObject gameObject in GameObjects.ToList())
        {
            gameObject.Update();
        }

        Camera.Update();
    }

    public List<GameObject> GetGameObjects()
    {
        List<GameObject> foundGameObjects = new List<GameObject>();

        foreach (GameObject gameObject in GameObjects)
        {
            foundGameObjects.Add(gameObject);

            if (!gameObject.HasComponent<TileGrid>()) continue;

            List<TileGrid> tileGrids = gameObject.GetComponents<TileGrid>();

            foreach (TileGrid tileGrid in tileGrids)
            {
                foreach (Tile tile in tileGrid.GetTiles())
                {
                    foundGameObjects.Add(tile);
                }
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
            if(gameObject.Position == position)
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
        gameObject.Load();
    }

    public void AddUIElement(GameObject uiElement)
    {
        UIElements.Add(uiElement);
        uiElement.Load();
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

    public void Pause()
    {
        Paused = true;
    }
}
