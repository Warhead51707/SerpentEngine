using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;
public class ComponentList
{
    public GameObject GameObject { get; private set; }
    public List<Component> Components { get; private set; } = new List<Component>();

    public ComponentList(GameObject gameObject = null)
    {
        GameObject = gameObject;
    }

    public void Draw()
    {
        foreach (Component component in Components)
        {
            
            if (component.Visible)
            {
                component.Draw();
            }
        }
    }

    public void SetGameObject(GameObject gameObject)
    {
        GameObject = gameObject;
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
        foreach (Component component in Components)
        {
            if (component is T)
            {
                return (T)component;
            }
        }

        return null;
    }
}
