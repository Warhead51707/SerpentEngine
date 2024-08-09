﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;
public class Tile : GameObject
{
    public Tile(string name)
    {
        Name = name;
    }

    public static new Tile Empty()
    {
        return new Tile("");
    }


    public override void Update()
    {
        base.Update();
    }
}
