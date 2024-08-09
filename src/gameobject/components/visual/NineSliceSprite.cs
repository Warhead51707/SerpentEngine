using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;
public class NineSliceSprite : Sprite
{
    public int Padding { get; private set; } = 1;
    public new Vector2 Size { get; set; } = Vector2.Zero;

    private Rectangle[] sourcePatches;
    private Rectangle[] targetPatches;
    public NineSliceSprite(string path) : base(path)
    {
        Size = new Vector2(texture2d.Width, texture2d.Height);

        sourcePatches = CreateSourcePatches();
    }

    public void SetPadding(int padding)
    {
        Padding = padding;
        sourcePatches = CreateSourcePatches();
    }

    private Rectangle[] CreateSourcePatches()
    {
        int x = texture2d.Bounds.X;
        int y = texture2d.Bounds.Y;
        int width = texture2d.Bounds.Width;
        int height = texture2d.Bounds.Height;

        int centerWidth = width - Padding * 2;
        int centerHeight = height - Padding * 2;

        int topY = y + Padding;
        int bottomY = y + height - Padding;

        int leftX = x + Padding;
        int rightX = x + width - Padding;

        Rectangle[] patches = new[]
        {
            new Rectangle(x, y, Padding, Padding), // top left
            new Rectangle(leftX, y, centerWidth, Padding), // top middle
            new Rectangle(rightX, y, Padding, Padding), // top right
            new Rectangle(x, topY, Padding, centerHeight), // middle left
            new Rectangle(leftX, topY, centerWidth, centerHeight), // middle
            new Rectangle(rightX, topY, Padding, centerHeight), // middle right
            new Rectangle(x, bottomY, Padding, Padding), // bottom left
            new Rectangle(leftX, bottomY, centerWidth, Padding), // bottom middle
            new Rectangle(rightX, bottomY, Padding, Padding) // bottom right
        };

        return patches;
    }

    private Rectangle[] CreateTargetPatches()
    {
        int x = (int)GameObject.Position.X;
        int y = (int)GameObject.Position.Y;
        int width = (int)Size.X;
        int height = (int)Size.Y;

        int centerWidth = width - Padding * 2;
        int centerHeight = height - Padding * 2;

        int topY = y + Padding;
        int bottomY = y + height - Padding;

        int leftX = x + Padding;
        int rightX = x + width - Padding;

        Rectangle[] patches = new[]
        {
            new Rectangle(x, y, Padding, Padding), // top left
            new Rectangle(leftX, y, centerWidth, Padding), // top middle
            new Rectangle(rightX, y, Padding, Padding), // top right
            new Rectangle(x, topY, Padding, centerHeight), // middle left
            new Rectangle(leftX, topY, centerWidth, centerHeight), // middle
            new Rectangle(rightX, topY, Padding, centerHeight), // middle right
            new Rectangle(x, bottomY, Padding, Padding), // bottom left
            new Rectangle(leftX, bottomY, centerWidth, Padding), // bottom middle
            new Rectangle(rightX, bottomY, Padding, Padding) // bottom right
        };

        return patches;
    }

    public override void Draw()
    {
        targetPatches = CreateTargetPatches();

        for (int i = 0; i < sourcePatches.Length; i++)
        {
            float sourceWidth = (float)sourcePatches[i].Width;
            float sourceHeight = (float)sourcePatches[i].Height;

            float targetWidth = (float)targetPatches[i].Width;
            float targetHeight = (float)targetPatches[i].Height;

            Vector2 scale = new Vector2(targetWidth / sourceWidth, targetHeight / sourceHeight);

            SerpentEngine.Draw.SpriteBatch.Draw(
                texture2d,
                new Vector2(targetPatches[i].X, targetPatches[i].Y),
                sourcePatches[i],
                Color,
                Rotation,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                GameObject.Layer * 0.001f
            );
        }
    }
}
