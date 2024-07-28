using Microsoft.Xna.Framework;

namespace SerpentEngine;
public class GameObject
{
    public ComponentList Components { get; private set; }
    public Vector2 Position { get; set; } = Vector2.Zero;
    public float Layer { get; set; } = 0;

    public GameObject()
    {
        Components = new ComponentList(this);
    }

    public virtual void Load()
    {
    }

    public virtual void Update()
    {
        Components.Update();
    }

    public void Draw()
    {
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
        return Components.GetComponent<T>();
    }
}
