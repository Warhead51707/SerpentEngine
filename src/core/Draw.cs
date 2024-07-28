using Microsoft.Xna.Framework.Graphics;

namespace SerpentEngine;
public static class Draw
{
    // For all drawing operations
    public static SpriteBatch SpriteBatch { get; private set; }

    internal static void Initialize(GraphicsDevice graphicsDevice)
    {
        SpriteBatch = new SpriteBatch(graphicsDevice);
    }
}
