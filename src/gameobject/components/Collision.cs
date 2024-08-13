using Microsoft.Xna.Framework;

namespace SerpentEngine;

public delegate void CollisionEvent(GameObject target);
public class Collision : Component
{
    public event CollisionEvent OnCollide;
    public Rectangle Box { get; private set; } = Rectangle.Empty;

    public Collision(Vector2 position, Vector2 dimensions) : base(false)
    {
        Box = new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
    }

    public static Collision Empty()
    {
        return new Collision(Vector2.Zero, Vector2.Zero);
    }

    public override void Update()
    {
        base.Update();

        Box = new Rectangle((int)GameObject.Position.X, (int)GameObject.Position.Y, Box.Width, Box.Height);

        CheckCollision();
    }

    private void CheckCollision()
    {
        foreach (GameObject target in SceneManager.CurrentScene.GameObjects)
        {
            Box = new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
        }

        public static Collision Empty()
        {
            return new Collision(Vector2.Zero, Vector2.Zero);
        }

        public override void Update()
        {
            base.Update();

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
                    OnCollide?.Invoke(target);
                    break;
                }
            }
        }
    }
}
    