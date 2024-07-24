using Microsoft.Xna.Framework;
using SerpentEngine.src.util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;

public class SpriteSheet : Component
{
    public Sprite CurrentSprite { get; set; }
    public Vector2 Size { get; set; } = Vector2.Zero;
    public Vector2 TileSize { get; set; } = Vector2.Zero;

    public SpriteSheet(string path, Vector2 tileSize, Vector2 size) : base(true)
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

    public void ChangeCoordinates(int coordinates)
    {
        CurrentSprite.Coordinates = VectorHelper.ToCoordinates(coordinates, Size);
    }

    public override void Draw()
    {
        CurrentSprite.Draw(TileSize, Size);
    }
}
