using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SerpentEngine;
public class TileSet
{
    public List<Tile> Tiles { get; private set; } = new List<Tile>();

    public Dictionary<string, Func<Tile>> TileRegistry { get; private set; } = new Dictionary<string, Func<Tile>>();

    public void AddBySpritePath(string tileName, string spritePath)
    {
        Tile tile = new Tile(spritePath, tileName);

        Tiles.Add(tile);

        TileRegistry.Add(tileName, () =>
        {
            Tile newTile = new Tile(spritePath, tileName);

            return newTile;
        });
    }

    public void Add(string tileName, Func<Tile> tile)
    {
        TileRegistry.Add(tileName, tile);
    }

    public Tile GetNewInstance(string tileName)
    {
       Tile tile = TileRegistry[tileName]();

       return tile;
    } 
}
