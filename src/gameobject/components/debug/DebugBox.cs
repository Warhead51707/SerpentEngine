using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SerpentEngine;

namespace CastleGame;
public class DebugBox : Component
{
    public DebugBox() : base(true)
    {

    }

    public override void Draw()
    {
        if (!DebugStates.ShowButtonHitboxes) return;

        Rectangle bounds = new Rectangle((int)GameObject.Position.X - (int)(GameObject.Size.X / 2), (int)GameObject.Position.Y - (int)(GameObject.Size.Y / 2), (int)GameObject.Size.X, (int)GameObject.Size.Y);

        Color color = Color.Pink;

        color.A = 100;
        Texture2D texture2d = SerpentEngine.Draw.Pixel;

        SerpentEngine.Draw.SpriteBatch.Draw(texture2d, bounds, null, color, 0, Vector2.Zero, SpriteEffects.None, 100 * 0.001f);
    }
}
