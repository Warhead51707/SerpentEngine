using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SerpentEngine;

public delegate void CollisionEvent(GameObject target);
public class Collision : Component
{
    public event CollisionEvent OnCollide;
    public Rectangle Box { get; set; } = Rectangle.Empty;

    public GameObject CollidingGameObject { get; set; }

    public Collision(Vector2 position, Vector2 dimensions) : base(true)
    {
        Box = new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
    }

    public static Collision Empty()
    {
        
        return new Collision(Vector2.Zero,Vector2.Zero);
    }

    public override void Update()
    {
        base.Update();

        CollidingGameObject = null;

        Box = new Rectangle((int)GameObject.Position.X, (int)GameObject.Position.Y, Box.Width, Box.Height);

        CheckCollision();
    }

    private void CheckCollision()
    {
        
        foreach (GameObject target in SceneManager.CurrentScene.GetGameObjects())
        {
            if (!target.HasComponent<Collision>()) continue;

            if (GameObject == target) continue;

            Collision targetCollision = target.GetComponent<Collision>();

            if (Box.Intersects(targetCollision.Box))
            {
                CollidingGameObject = target;
                OnCollide?.Invoke(target);
                break;
            }
        }
    }

    public override void Draw()
    {

        if (!DebugStates.ShowCollisionBoxes) return;

        Rectangle bounds = new Rectangle((int)Box.X - (int)(Box.Width / 2), (int)Box.Y - (int)(Box.Height / 2), (int)Box.Width, (int)Box.Height);

        Color color = Color.Blue;

        color.A = 50;
        Texture2D texture2d = SerpentEngine.Draw.Pixel;

        SerpentEngine.Draw.SpriteBatch.Draw(texture2d, bounds, null, color, 0, Vector2.Zero, SpriteEffects.None, 100 * 0.001f);

        base.Draw();
    }

}
    