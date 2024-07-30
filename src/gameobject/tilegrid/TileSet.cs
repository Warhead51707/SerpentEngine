using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SerpentEngine;
public class TileSet
{
    public List<Tile> Tiles { get; private set; } = new List<Tile>();

    public Dictionary<string, Func<Tile>> TileRegistry { get; private set; } = new Dictionary<string, Func<Tile>>();

    public void Add(string tileName, Func<Tile> tile)
    {
        TileRegistry.Add(tileName, tile);
    }

    public void AddFromSprite(string tileName, string spritePath)
    {
        Tile tile = new Tile(tileName);

        Tiles.Add(tile);

        Add(tileName, () =>
        {
            Tile newTile = new Tile(tileName);

            Sprite sprite = new Sprite(spritePath);
            newTile.AddComponent(sprite);

            return newTile;
        });
    }

    public void AddFromSpriteSheet(string sheetName, SpriteSheet spriteSheet)
    {
        for (int y = 0; y <= spriteSheet.Size.Y; y++)
        {
            for (int x = 0; x <= spriteSheet.Size.X; x++)
            {
                spriteSheet.CurrentSprite.Coordinates = new Vector2(x, y);

                Sprite sprite = spriteSheet.CurrentSprite.Clone();

                Tile tile = new Tile(sheetName + "_" + x + "_" + y);

                Tiles.Add(tile);

                Add(sheetName + "_" + x + "_" + y, () =>
                {
                    Tile newTile = new Tile(sheetName + "_" + x + "_" + y);

                    newTile.AddComponent(sprite.Clone());

                    return newTile;
                });
            }
        }
    }

    public Tile GetNewInstance(string tileName)
    {
       Tile tile = TileRegistry[tileName]();

       return tile;
    } 
}
