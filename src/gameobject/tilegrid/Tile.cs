using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;
public class Tile : GameObject
{
    public string Name { get; private set; }
    public Sprite Sprite { get; private set; }

    public Tile(Sprite sprite, string name)
    {
        Sprite = sprite;
        Name = name;

        AddComponent(Sprite);
    }

    public Tile(string name)
    {
        Name = name;
    }
}
