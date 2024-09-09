using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SerpentEngine;
public class TileGrid : Component
{
    public List<TileSet> TileSets { get; private set; } = new List<TileSet>();
    public Dictionary<Vector2, Tile> Tiles { get; private set; } = new Dictionary<Vector2, Tile>();
    public Vector2 TileSize { get; private set; }
    public int VisibleTiles { get; private set; }
    public int Layer { get; set; } = 0;

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
        TileSet matchedTileSet = null;

        foreach (TileSet tileSet in TileSets)
        {
            foreach (KeyValuePair<string, Func<Tile>> tile in tileSet.TileRegistry)
            {
                Tile tile_ = tile.Value();
                if (tile_.Name == tileName)
                {
                    matchedTileSet = tileSet;
                    break;
                }
            }

            if (matchedTileSet != null)
            {
                break;
            }
        }

        Tile placedTile = matchedTileSet.GetNewInstance(tileName);
        placedTile.Load();

        placedTile.Layer = Layer;

        placedTile.Position = ConvertGridCoordinatesToWorldCoordinates(coordinates);

        if (Tiles.ContainsKey(coordinates))
        {
            Tiles[coordinates] = placedTile;
        }
        else
        {
            Tiles.Add(coordinates, placedTile);


        }

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
        Tile tile = Tiles[coordinates];

        tile.OnRemove();

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

    public List<Tile> GetTiles()
    {
        List<Tile> tiles = new List<Tile>();

        foreach (KeyValuePair<Vector2, Tile> tileEntry in Tiles)
        {
            Tile tile = tileEntry.Value;

            tiles.Add(tile);
        }

        return tiles;
    }

    public Tile GetTileFromGridCoordinates(Vector2 coordinates)
    {
        foreach (KeyValuePair<Vector2, Tile> tileEntry in Tiles)
        {
            if(ConvertWorldCoordinatesToGridCoordinates(tileEntry.Value.Position) == coordinates)
            {
                Tile tile = tileEntry.Value;
                return tile;
            }
        }

        return null;
    }

    public Tile GetTileFromWorldCoordinates(Vector2 coordinates)
    {
        foreach (KeyValuePair<Vector2, Tile> tileEntry in Tiles)
        {
            if (ConvertWorldCoordinatesToGridCoordinates(tileEntry.Value.Position) == ConvertWorldCoordinatesToGridCoordinates(coordinates))
            {
                Tile tile = tileEntry.Value;
                return tile;
            }
        }

        return null;
    }

    public Vector2 ConvertWorldCoordinatesToGridCoordinates(Vector2 worldCoordinates)
    {
       return new Vector2((int)(worldCoordinates.X / TileSize.X), (int)(worldCoordinates.Y / TileSize.Y));
    }

    public Vector2 ConvertGridCoordinatesToWorldCoordinates(Vector2 gridCoordinates)
    {
        return new Vector2(gridCoordinates.X * TileSize.X, gridCoordinates.Y * TileSize.Y);
    }

    public override void Update()
    {

        VisibleTiles = 0;

        Camera camera = SceneManager.CurrentScene.Camera;

        Vector2 cameraPosition = camera.GetScreenPostion();
        Vector2 cameraGridPosition = ConvertWorldCoordinatesToGridCoordinates(cameraPosition);

        float cameraWidth = camera.Viewport.Width / camera.Zoom;
        float cameraHeight = camera.Viewport.Height / camera.Zoom;

        int horizontalTileCount = (int)(cameraWidth / TileSize.X) + 4;
        int verticalTileCount = (int)(cameraHeight / TileSize.Y) + 4;

        int horizontalStart = (int)Math.Ceiling(-cameraGridPosition.X / camera.Zoom) - 2;
        int verticalStart = (int)Math.Ceiling(-cameraGridPosition.Y / camera.Zoom) - 2;

        for (int y = 0; y < verticalTileCount; y++)
        {
            for (int x = 0; x < horizontalTileCount; x++)
            {
                Vector2 gridCoordinates = new Vector2(horizontalStart + x, verticalStart + y);

                if (Tiles.ContainsKey(gridCoordinates))
                {
                    Tiles[gridCoordinates].Update();
                    VisibleTiles++; 
                }
            }
        }
    }

    public Tile South(Tile tile)
    {
        if (!Tiles.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(tile.Position) + new Vector2(0, 1))) return Tile.Empty();

        return Tiles[ConvertWorldCoordinatesToGridCoordinates(tile.Position) + new Vector2(0, 1)];
    }

    public Tile North(Tile tile)
    {
        if (!Tiles.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(tile.Position) + new Vector2(0, -1))) return Tile.Empty();

        return Tiles[ConvertWorldCoordinatesToGridCoordinates(tile.Position) + new Vector2(0, -1)];
    }

    public Tile East(Tile tile)
    {
        if (!Tiles.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(tile.Position) + new Vector2(1, 0))) return Tile.Empty();

        return Tiles[ConvertWorldCoordinatesToGridCoordinates(tile.Position) + new Vector2(1, 0)];
    }

    public Tile West(Tile tile)
    {
        if (!Tiles.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(tile.Position) + new Vector2(-1, 0))) return Tile.Empty();

        return Tiles[ConvertWorldCoordinatesToGridCoordinates(tile.Position) + new Vector2(-1, 0)];
    }

    public Tile South(Tile tile, int tiles)
    {
        if (!Tiles.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(tile.Position) + new Vector2(0, tiles))) return Tile.Empty();

        return Tiles[ConvertWorldCoordinatesToGridCoordinates(tile.Position) + new Vector2(0, tiles)];
    }

    public Tile North(Tile tile, int tiles)
    {
        if (!Tiles.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(tile.Position) + new Vector2(0, -tiles))) return Tile.Empty();

        return Tiles[ConvertWorldCoordinatesToGridCoordinates(tile.Position) + new Vector2(0, -tiles)];
    }

    public Tile East(Tile tile, int tiles)
    {
        if (!Tiles.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(tile.Position) + new Vector2(tiles, 0))) return Tile.Empty();

        return Tiles[ConvertWorldCoordinatesToGridCoordinates(tile.Position) + new Vector2(tiles, 0)];
    }

    public Tile West(Tile tile, int tiles)
    {
        if (!Tiles.ContainsKey(ConvertWorldCoordinatesToGridCoordinates(tile.Position) + new Vector2(-tiles, 0))) return Tile.Empty();

        return Tiles[ConvertWorldCoordinatesToGridCoordinates(tile.Position) + new Vector2(-tiles, 0)];
    }

    public override void Draw()
    {
        Camera camera = SceneManager.CurrentScene.Camera;

        Vector2 cameraPosition = camera.GetScreenPostion();
        Vector2 cameraGridPosition = ConvertWorldCoordinatesToGridCoordinates(cameraPosition);

        float cameraWidth = camera.Viewport.Width / camera.Zoom;
        float cameraHeight = camera.Viewport.Height / camera.Zoom;

        int horizontalTileCount = (int)(cameraWidth / TileSize.X) + 4;
        int verticalTileCount = (int)(cameraHeight / TileSize.Y) + 4;

        int horizontalStart = (int)Math.Ceiling(-cameraGridPosition.X / camera.Zoom) - 2;
        int verticalStart = (int)Math.Ceiling(-cameraGridPosition.Y / camera.Zoom) - 2;

        for (int y = 0; y < verticalTileCount; y++)
        {
            for (int x = 0; x < horizontalTileCount; x++)
            {
                Vector2 gridCoordinates = new Vector2(horizontalStart + x, verticalStart + y);

                if (Tiles.ContainsKey(gridCoordinates))
                {
                    Tiles[gridCoordinates].Draw();
                }
            }
        }
    }
}
