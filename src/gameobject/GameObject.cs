using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace SerpentEngine;
public class GameObject
{
    public ComponentList Components { get; private set; }
    public string Name { get; set; } = "";
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Vector2 Size { get; set; } = Vector2.Zero;
    public bool Enabled { get; set; } = true;
    public bool Visible { get; set; } = true;
    public float Layer { get; set; } = 0;

    public GameObject()
    {
        Components = new ComponentList(this);
    }

    public void Initialize()
    {
        Load();

        Components.Initialize();
    }

    public virtual void Load()
    {
    }

    public virtual void Update()
    {
        if (!Enabled) return;

        Components.Update();
    }

    public virtual void Draw()
    {
        if (!Enabled) return;

        if (!Visible) return;

        Components.Draw();
    }

    public static GameObject Empty()
    {
        return new GameObject();
    }

    public T CreateAndAddComponent<T>() where T : Component, new()
    {
        AddComponent(new T());

        return GetComponent<T>();
    }

    public void AddComponent(Component component)
    {
        Components.AddComponent(component);
    }

    public void AddComponents(params Component[] components)
    {
        foreach (Component component in components)
        {
            AddComponent(component);
        }
    }

    public void RemoveComponent(Component component)
    {
        Components.RemoveComponent(component);
    }

    public T GetComponent<T>() where T : Component
    {
        Component foundComponent = Components.GetComponent<T>();


        return foundComponent as T;
    }

    public List<T> GetComponents<T>() where T : Component
    {
        return Components.GetComponents<T>();
    }

    public bool HasComponent<T>() where T : Component
    {
        return Components.GetComponent<T>() != null;
    }

    public virtual void OnRemove()
    {
    }

    public void Remove()
    {
        OnRemove();

        SceneManager.CurrentScene.Remove(this);
    }
}
