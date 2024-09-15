using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;

public delegate void ClickEvent();
public class Button : Component
{
    public event ClickEvent OnClick;
    public Vector2 Size { get; set; } = Vector2.Zero;
    public Rectangle Hitbox { get; set; } = Rectangle.Empty;
    public Button(Vector2 size) : base(true)
    {
        Size = size;
    }

    public override void Initialize()
    {
        Hitbox = new Rectangle((int)GameObject.Position.X - (int)(Size.X / 2), (int)GameObject.Position.Y - (int)(Size.Y / 2), (int)Size.X, (int)Size.Y);
    }

    public override void Update()
    {
        CheckClick();
        base.Update();
    }

    public override void Draw()
    {
        if (!DebugStates.ShowButtonHitboxes) return;

        Color color = Color.Pink;

        color.A = 100;

        SerpentEngine.Draw.SpriteBatch.Draw(SerpentEngine.Draw.Pixel, Hitbox, color);
    }

    public void CheckClick()
    {
        Vector2 screenPosition = Input.Mouse.GetNewPosition() / SceneManager.CurrentScene.Camera.UIScale;
        if (!Input.Mouse.LeftClick()) return;
        
        if (Hitbox.Contains(screenPosition))
        {
            OnClick.Invoke();
        }

    }
}
