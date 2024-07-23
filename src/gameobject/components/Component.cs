using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;
public abstract class Component
{
    public GameObject GameObject { get; private set; }
    public ComponentList SubComponents { get; private set; } = new ComponentList();
    public bool Enabled { get; set; } = true;
    public bool Visible { get; private set; } = true;

    public Component(bool visible)
    {
        Visible = visible;
    }

    public void Add(GameObject gameObject)
    {
        GameObject = gameObject;
        SubComponents.SetGameObject(gameObject);
    }

    public virtual void Update() { }

    public virtual void Draw()
    {
        SubComponents.Draw();
    }
}
