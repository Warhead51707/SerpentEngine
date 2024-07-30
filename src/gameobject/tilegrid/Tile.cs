using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;
public class Tile : GameObject
{
    public string Name { get; private set; }

    public Tile(string name)
    {
        Name = name;
    }

    public override void Update()
    {
        base.Update();
    }
}
