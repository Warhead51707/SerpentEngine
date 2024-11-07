using Microsoft.Xna.Framework;

namespace SerpentEngine;

public delegate void CollisionEvent(GameObject target);
public class Collision : Component
{
    public event CollisionEvent OnCollide;
    public Rectangle Box { get; set; } = Rectangle.Empty;

    public GameObject CollidingGameObject { get; set; }

    public Collision(Vector2 position, Vector2 dimensions) : base(false)
    {
        Box = new Rectangle((int)position.X - (int)(dimensions.X / 2), (int)position.Y - (int)(dimensions.Y / 2), (int)dimensions.X, (int)dimensions.Y);
    }

    public static Collision Empty()
    {
        
        return new Collision(Vector2.Zero,Vector2.Zero);
    }

    public override void Update()
    {
        base.Update();

        CollidingGameObject = null;

        Box = new Rectangle((int)GameObject.Position.X - (Box.Width / 2), (int)GameObject.Position.Y - (Box.Height / 2), Box.Width, Box.Height);

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

}
    