using Microsoft.Xna.Framework;
using static System.Formats.Asn1.AsnWriter;
using System.IO;

namespace SerpentEngine;

public class SpriteSheet : Component
{
    public Sprite CurrentSprite { get; set; }
    public Vector2 Size { get; set; } = Vector2.Zero;
    public Vector2 TileSize { get; set; } = Vector2.Zero;

    public SpriteSheet(string path, Vector2 tileSize) : base(true)
    {
        TileSize = tileSize;

        CurrentSprite = new Sprite(path);

        Size = CurrentSprite.Size / TileSize;

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
