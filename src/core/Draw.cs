using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SerpentEngine;
public static class Draw
{
    // For all drawing operations
    public static SpriteBatch SpriteBatch { get; private set; }

    public static Texture2D Pixel { get; private set; }

    internal static void Initialize(GraphicsDevice graphicsDevice)
    {
        SpriteBatch = new SpriteBatch(graphicsDevice);
        Pixel = new Texture2D(graphicsDevice, 1, 1);
        Pixel.SetData(new[] { Color.White });
    }
}
