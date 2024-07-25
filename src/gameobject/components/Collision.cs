using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine
{
    public class Collision : Component
    {
        public Rectangle Box { get; set; } = Rectangle.Empty;
        public Collision(Vector2 position ,Vector2 dimensions) : base(false)
        {
            Box = new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
        }

        public override void Update()
        {
            base.Update();

            
        }

        public bool GetCollision(GameObject target)
        {
            if (Box.Intersects(target.GetComponent<Collision>().Box))
            {
                return true;
            }

            return false;
        }

    }
}
