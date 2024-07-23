﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;

public class Spritesheet : Component
{

    public Sprite CurrentSprite { get; set; }
    public Vector2 Size { get; set; } = Vector2.Zero;
    public Vector2 TileSize { get; set; } = Vector2.Zero;


    public Spritesheet(string path, Vector2 tileSize, Vector2 size) : base(true)
    {
        Size = size;
        TileSize = tileSize;

        CurrentSprite = new Sprite(path);

        SubComponents.AddComponent(CurrentSprite);

    }

    public void ChangeCoordinates(Vector2 coordinates)
    {
        CurrentSprite.Coordinates = coordinates;
    }


    public override void Draw()
    {
        CurrentSprite.Draw(TileSize, Size);
    }









}
