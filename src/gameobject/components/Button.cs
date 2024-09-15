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
    public Button(Vector2 size) : base(false)
    {
        Size = size;
    }

    public override void Update()
    {
        CheckClick();
        base.Update();
    }

    public void CheckClick()
    {
        Rectangle box = new Rectangle((int)GameObject.Position.X - (int)(Size.X / 2), (int)GameObject.Position.Y - (int)(Size.Y / 2), (int)Size.X, (int)Size.Y);

        Vector2 screenPosition = Input.Mouse.GetNewPosition() / SceneManager.CurrentScene.Camera.UIScale;

        if (!Input.Mouse.LeftClick()) return;
        
        if (box.Contains(screenPosition))
        {
            OnClick.Invoke();
        }

    }
}
