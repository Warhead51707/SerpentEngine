using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;

public class UiElementGroup
{

    public GameObject Parent = GameObject.Empty();

    public Dictionary<GameObject, Vector2> Children = new Dictionary<GameObject, Vector2>();

    public UiElementGroup(GameObject parent)
    {
        Parent = parent;
    }

    public void AddChild(GameObject child, Vector2 offset)
    {
        child.Position = Parent.Position + offset;
        Children.Add(child, offset);
    }
}
