using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;
public class GameObject
{
    public ComponentList Components { get; private set; }

    public Vector2 Position { get; set; } = Vector2.Zero;

    public GameObject()
    {
        Components = new ComponentList(this);
    }

    public virtual void Load()
    {
    }

    public virtual void Update()
    {
    }

    public void AddComponent(Component component)
    {
        Components.AddComponent(component);
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
