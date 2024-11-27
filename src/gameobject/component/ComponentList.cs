using System.Collections.Generic;
using System.Linq;

namespace SerpentEngine;
public class ComponentList
{
    public GameObject GameObject { get; private set; }
    public List<Component> Components { get; private set; } = new List<Component>();

    public ComponentList(GameObject gameObject = null)
    {
        GameObject = gameObject;
    }

    public void Initialize()
    {
        foreach (Component component in Components)
        {
            component.Initialize();
        }
    }

    public void Update()
    {
        foreach (Component component in Components.ToList())
        {
            component.Update();
        }
    }

    public void Draw()
    {
        foreach (Component component in Components)
        {
            
            if (component.Drawable)
            {
                component.Draw();
            }
        }
    }

    public void SetGameObject(GameObject gameObject)
    {
        GameObject = gameObject;

        foreach (Component component in Components)
        {
            component.Add(gameObject);
        }
    }

    public void AddComponent(Component component)
    {
        Components.Add(component);

        component.Add(GameObject);
    }

    public void RemoveComponent(Component component)
    {
        Components.Remove(component);
    }

    public T GetComponent<T>() where T : Component
    {
        return Components.FirstOrDefault(component => component is T) as T;
    }

    public List<T> GetComponents<T>() where T : Component
    {
        List<T> components = new List<T>();

        foreach (Component component in Components)
        {
            if (component is T)
            {
                components.Add((T)component);
            }
        }

        return components;
    }
}
