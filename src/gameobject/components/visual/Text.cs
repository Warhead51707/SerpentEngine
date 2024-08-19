using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace SerpentEngine
{
    public class Text : Component
    {
        public string Body { get; private set; }
        public SpriteFont Font { get; private set; }
        public float Rotation { get; set; } = 0f;

        public Color Color = Color.Black;
        public float Scale { get; set; } = 0f;
        public Text(string path, string text) : base(true)
        {
            Body = text;
            Font = SerpentGame.Instance.Content.Load<SpriteFont>(path);

        }


        public void AddText(string text)
        {
            Body += text;
        }

        public void ChangeText(string text)
        {
            Body = text;
        }


        public override void Draw()
        {
            if (!Enabled) return;

            SerpentEngine.Draw.SpriteBatch.DrawString(Font, Body, GameObject.Position, Color, Rotation, Vector2.Zero, Scale, SpriteEffects.None, GameObject.Layer * 0.001f);
        }
    }
}
