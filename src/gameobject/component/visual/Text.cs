﻿using Microsoft.Xna.Framework;
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
        public Vector2 Position { get; set; } = Vector2.Zero;
        public string Body { get; private set; }
        public SpriteFont Font { get; private set; }
        public float Rotation { get; set; } = 0f;
        public Color Color = Color.Black;
        public float Scale { get; set; } = 1f;
        public Vector2 Size => Font.MeasureString(Body) * Scale;

        public float LayerOffset { get; set; } = 0;

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

            SerpentEngine.Draw.SpriteBatch.DrawString(Font, Body, GameObject.Position + Position, Color, Rotation, Vector2.Zero, Scale, SpriteEffects.None, (GameObject.Layer + LayerOffset) * 0.001f);
        }
    }
}
