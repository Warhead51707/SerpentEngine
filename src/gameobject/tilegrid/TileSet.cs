using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SerpentEngine;
public class TileSet
{
    public List<Tile> Tiles { get; private set; } = new List<Tile>();

    private Dictionary<string, Func<Tile>> tileRegistry = new Dictionary<string, Func<Tile>>();

    public void Add(Tile tile)
    {
        Tiles.Add(tile);
    }

    public void Add(string tileName, Func<Tile> tile)
    {
        tileRegistry.Add(tileName, tile);
    }

    public void AddFromSprite(string tileName, string spritePath)
    {
        Sprite sprite = new Sprite(spritePath);

        Tile tile = new Tile(sprite, tileName);

        Tiles.Add(tile);

        Add(tileName, () => new Tile(sprite, tileName));

        Debug.WriteLine("Added tile: " + tileName);
    }

    public void AddFromSpriteSheet(string sheetName, SpriteSheet spriteSheet)
    {
        for (int y = 0; y <= spriteSheet.Size.Y; y++)
        {
            for (int x = 0; x <= spriteSheet.Size.X; x++)
            {
                spriteSheet.CurrentSprite.Coordinates = new Vector2(x, y);

                Sprite sprite = spriteSheet.CurrentSprite.Clone();

                Tile tile = new Tile(sprite, sheetName + "_" + x + "_" + y);

                Tiles.Add(tile);
            }
        }
    }

    public Tile GetNewInstance(string tileName)
    {
       Tile tile = tileRegistry[tileName]();

       return tile;
    } 
}
