using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine
{

    public delegate void CollisionEvent(GameObject target);
    public class Collision : Component
    {

        public event CollisionEvent OnCollide;
        public Rectangle Box { get; set; } = Rectangle.Empty;

        public Collision(Vector2 position ,Vector2 dimensions) : base(false)
        {
            Box = new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
        }

        public override void Update()
        {
            base.Update();

            
        }

        public bool CheckCollision()
        {
            foreach (GameObject target in SceneManager.CurrentScene.GameObjects)
            {
                if (target.GetComponent<Collision>() != null)
                {
                    if (Box.Intersects(target.GetComponent<Collision>().Box))
                    {
                        OnCollide?.Invoke(target);

                        return true;
                    }
                }
            }

            return false;
        }

        public static Collision Empty()
        {
            return new Collision(Vector2.Zero, Vector2.Zero);
        }

    }
}
