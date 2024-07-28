using System.Collections.Generic;

namespace SerpentEngine;
public class SceneManager
{
    // All Scenes in the game
    public Dictionary<string, Scene> Scenes { get; private set; } = new Dictionary<string, Scene>();

    public static Scene CurrentScene { get; private set; }

    public SceneManager()
    {
    }

    public void AddScene(Scene scene)
    {
        Scenes.Add(scene.Name, scene);
    }

    public void Remove(Scene scene)
    {
        Scenes.Remove(scene.Name);
    }

    public void SetCurrentScene(Scene scene)
    {
        if (CurrentScene != null)
        {
            CurrentScene.End();
        }

        CurrentScene = scene;

        CurrentScene.LoadContent();

        CurrentScene.Begin();
    }

    public void Update()
    {
        CurrentScene.Update();
    }

    public void Draw()
    {
        CurrentScene.Draw();
    }
}
