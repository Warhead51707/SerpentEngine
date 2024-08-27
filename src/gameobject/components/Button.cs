using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;

public delegate void ClickEvent(Vector2 position);
public class Button : Component
{
    public event ClickEvent OnClick;
    public Vector2 Size { get; set; } = Vector2.Zero;
    public Button(Vector2 size) : base(false)
    {
        size = Size;
    }

    public override void Update()
    {

        CheckClick();
        base.Update();
    }

    public void CheckClick()
    {
        Rectangle box = new Rectangle((int)GameObject.Position.X, (int)GameObject.Position.Y, (int)Size.X, (int)Size.Y);
        Vector2 screenPosition = Input.Mouse.GetNewPosition();
        Vector2 screenCenter = new Vector2(GraphicsConfig.SCREEN_WIDTH / 2, GraphicsConfig.SCREEN_HEIGHT / 2);
        Vector2 worldPosition = (screenPosition - screenCenter) / SceneManager.CurrentScene.Camera.Zoom + SceneManager.CurrentScene.Camera.Position;

        if (!Input.Mouse.LeftClick()) return;
        
        DebugGui.Log(new Vector2((int)GameObject.Position.X, (int)GameObject.Position.Y) + "     " + worldPosition);
        if (box.Contains(worldPosition))
        {
            OnClick.Invoke(worldPosition);
        }

    }



}
