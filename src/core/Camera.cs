﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;
public class Camera
{
    public float Zoom { get; set; } = 1.0f;
    public Vector2 Position { get; private set; } = Vector2.Zero;
    public Viewport Viewport { get; private set; }
    public Matrix Matrix { get; private set; } = Matrix.Identity;

    private GameObject target;

    public Camera()
    {
        Viewport = new Viewport(0, 0, GraphicsConfig.SCREEN_WIDTH, GraphicsConfig.SCREEN_HEIGHT);
    }

    public void Update()
    {
        Position = target.Position;
        Matrix = Matrix.CreateTranslation(-Position.X, -Position.Y, 0) * 
            Matrix.CreateScale(Zoom) * 
            Matrix.CreateTranslation(Viewport.Width / 2, Viewport.Height / 2, 0);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
        Position = target.Position;
    }

    public void ResetTarget()
    {
        target = null;
    }

    public Vector2 GetScreenPostion()
    {
        return new Vector2(Matrix.Translation.X, Matrix.Translation.Y);
    }
}

