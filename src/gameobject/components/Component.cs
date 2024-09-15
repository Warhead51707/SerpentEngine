
using System.Diagnostics;

namespace SerpentEngine;
public abstract class Component
{
    public GameObject GameObject { get; private set; }
    public ComponentList SubComponents { get; private set; } = new ComponentList();
    public bool Enabled { get; set; } = true;
    public bool Drawable { get; private set; } = true;

    public Component(bool drawable)
    {
        Drawable = drawable;
    }

    public virtual void Add(GameObject gameObject)
    {
        GameObject = gameObject;
        SubComponents.SetGameObject(gameObject);
    }

    public virtual void Initialize()
    {
        SubComponents.Initialize();
    }

    public virtual void Update() {

    }

    public virtual void Draw()
    {
        SubComponents.Draw();
    }
}
