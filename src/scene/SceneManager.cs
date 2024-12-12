using System.Collections.Generic;

namespace SerpentEngine;
public class SceneManager
{
    // All Scenes in the game
    public static Dictionary<string, Scene> Scenes { get; private set; } = new Dictionary<string, Scene>();

    public static Scene CurrentScene { get; private set; }

    public SceneManager()
    {
    }

    private static void AddScene(Scene scene)
    {
        Scenes.Add(scene.Name, scene);
    }

    public static void RemoveScene(Scene scene)
    {
        Scenes.Remove(scene.Name);
    }

    public static void SetCurrentScene(Scene scene)
    {
        if (CurrentScene != null)
        {
            CurrentScene.End();
        }

        CurrentScene = scene;

        if (!Scenes.ContainsKey(scene.Name))
        {
            CurrentScene.LoadContent();
            AddScene(CurrentScene);
        } else
        {
            CurrentScene = Scenes.GetValueOrDefault(scene.Name);
        }

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
