using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;
public class Tile
{
    public string Name { get; private set; }
    public Vector2 Position { get; set; } = Vector2.Zero;

    public Vector2 Offset { get; set; } = Vector2.Zero;
    public Vector2 Scale { get; set; } = Vector2.One;
    public Vector2 Coordinates { get; set; } = Vector2.Zero;
    public float Rotation { get; set; } = 0f;
    public Color Color { get; set; } = Color.White;
    public float Layer { get; set; } = 0;
    public SpriteEffects Effect { get; set; } = SpriteEffects.None;

    public Texture2D texture2d;

    public Tile(string spritePath, string name)
    {
        Name = name;

        FileStream fileStream = new FileStream(spritePath + ".png", FileMode.Open, FileAccess.Read);
        texture2d = Texture2D.FromStream(SerpentGame.Instance.GraphicsDevice, fileStream);
        fileStream.Close();
    }

    public virtual void Draw()
    {
        SerpentEngine.Draw.SpriteBatch.Draw(texture2d, Position + Offset, new Rectangle((int)Coordinates.X, (int)Coordinates.Y, texture2d.Width, texture2d.Height), Color, Rotation, new Vector2(texture2d.Width / 2, texture2d.Height / 2), Scale, Effect, Layer * 0.001f);
    }
}
