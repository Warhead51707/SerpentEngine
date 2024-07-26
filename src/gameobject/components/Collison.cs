using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine
{

    public delegate void CollisionEvent(GameObject target);
    public class Collison : Component
    {

        public event CollisionEvent OnCollide;
        public Rectangle Box { get; set; } = Rectangle.Empty;

        public Collison(Vector2 position ,Vector2 dimensions) : base(false)
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
                if (target.GetComponent<Collison>() != null)
                {
                    if (Box.Intersects(target.GetComponent<Collison>().Box))
                    {
                        OnCollide?.Invoke(target);

                        return true;
                    }
                }
            }

            return false;
        }

        public static Collison Empty()
        {
            return new Collison(Vector2.Zero, Vector2.Zero);
        }

    }
}
