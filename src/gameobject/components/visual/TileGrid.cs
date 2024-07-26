using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;
public class TileGrid : Component
{
    public List<TileSet> TileSets { get; private set; } = new List<TileSet>();
    public Dictionary<Vector2, Tile> Tiles { get; private set; } = new Dictionary<Vector2, Tile>();
    public Vector2 TileSize { get; private set; }

    public TileGrid(Vector2 tileSize) : base(true)
    {
        TileSize = tileSize;
    }

    public void AddTileSet(TileSet tileSet)
    {
        TileSets.Add(tileSet);
    }

    public void PlaceTile(Vector2 coordinates, string tileName)
    {
        Tile tileSetTile = null;

        foreach (TileSet tileSet in TileSets)
        {
            foreach (Tile tile in tileSet.Tiles)
            {
                if (tile.Name == tileName)
                {
                    tileSetTile = tile;
                    break;
                }
            }

            if (tileSetTile != null)
            {
                break;
            }
        }

        Tile placedTile = tileSetTile.Clone();

        placedTile.Position = ConvertGridCoordinatesToWorldCoordinates(coordinates);

        Tiles.Add(coordinates, placedTile);
    }

    public void PlaceTiles(Vector2 startCoordinates, Vector2 endCoordinates, string tileName)
    {
        for (int y = (int)startCoordinates.Y; y <= endCoordinates.Y; y++)
        {
            for (int x = (int)startCoordinates.X; x <= endCoordinates.X; x++)
            {
                PlaceTile(new Vector2(x, y), tileName);
            }
        }
    }

    public void RemoveTile(Vector2 coordinates)
    {
        Tiles.Remove(coordinates);
    }

    public void RemoveTiles(Vector2 startCoordinates, Vector2 endCoordinates)
    {
        for (int y = (int)startCoordinates.Y; y <= endCoordinates.Y; y++)
        {
            for (int x = (int)startCoordinates.X; x <= endCoordinates.X; x++)
            {
                RemoveTile(new Vector2(x, y));
            }
        }
    }

    public Vector2 ConvertWorldCoordinatesToGridCoordinates(Vector2 worldCoordinates)
    {
       return new Vector2((int)(worldCoordinates.X / TileSize.X), (int)(worldCoordinates.Y / TileSize.Y));
    }

    public Vector2 ConvertGridCoordinatesToWorldCoordinates(Vector2 gridCoordinates)
    {
        return new Vector2(gridCoordinates.X * TileSize.X, gridCoordinates.Y * TileSize.Y);
    }

    public override void Draw()
    {
        foreach (Tile tile in Tiles.Values)
        {
            tile.Draw();
        }
    }
}
