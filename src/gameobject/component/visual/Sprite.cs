using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.IO;

namespace SerpentEngine;
public class Sprite : Component
{
    public string Path { get; private set; }
    public Vector2 Size { get; protected set; }

    // Sprite settings
    public Vector2 Scale { get; set; } = Vector2.One;
    public Vector2 Coordinates { get; set; } = Vector2.Zero;
    public float Rotation { get; set; } = 0f;
    public Color Color { get; set; } = Color.White;
    public float LayerOffset { get; set; } = 0;

    public SpriteEffects Effect { get; set; } = SpriteEffects.None;

    protected Texture2D texture2d;

    public Sprite(string path) : base(true)
    {

        Path = path;

        FileStream fileStream = new FileStream(path + ".png", FileMode.Open, FileAccess.Read);
        texture2d = Texture2D.FromStream(SerpentGame.Instance.GraphicsDevice, fileStream);
        fileStream.Close();

        Size = new Vector2(texture2d.Width, texture2d.Height);
    }

    public void ChangePath(string path)
    {
        Path = path;

        FileStream fileStream = new FileStream(path + ".png", FileMode.Open, FileAccess.Read);
        texture2d = Texture2D.FromStream(SerpentGame.Instance.GraphicsDevice, fileStream);
        fileStream.Close();

        Size = new Vector2(texture2d.Width, texture2d.Height);
    }

    public Sprite Clone()
    {
        Sprite sprite = new Sprite(Path);

        sprite.Scale = Scale;
        sprite.Coordinates = Coordinates;
        sprite.Rotation = Rotation;


        return sprite;
    }

    public override void Draw()
    {
        if (!Enabled) return;
        
        SerpentEngine.Draw.SpriteBatch.Draw(texture2d, GameObject.Position, new Rectangle((int)Coordinates.X, (int)Coordinates.Y, texture2d.Width, texture2d.Height), Color, Rotation, new Vector2(texture2d.Width / 2, texture2d.Height / 2), Scale, Effect, (GameObject.Layer + LayerOffset) * 0.001f);
    }

    public void Draw(Vector2 tileSize, Vector2 size)
    {
        if (!Enabled) return;

        SerpentEngine.Draw.SpriteBatch.Draw(texture2d, GameObject.Position, new Rectangle((int)Coordinates.X * (int)tileSize.X, (int)Coordinates.Y * (int)tileSize.Y, texture2d.Width / (int)size.X, texture2d.Height / (int)size.Y), Color, Rotation, new Vector2((texture2d.Width / (int)size.X) / 2, (texture2d.Height / (int)size.Y) / 2), Scale, Effect, (GameObject.Layer + LayerOffset) * 0.001f);
    }
}
