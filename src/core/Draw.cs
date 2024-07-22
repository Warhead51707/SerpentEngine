using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
