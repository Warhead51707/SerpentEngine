using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SerpentEngine;
public class SerpentMouse
{
    public bool LeftClickDrag { get; private set; } = false;
    public bool RightClickDrag { get; private set; } = false;

    private Vector2 newMousePos, oldMousePos, firstMousePos;
    private Vector2 newMouseAdjustedPos, systemCursorPos, screenLoc;
    private MouseState newMouse, oldMouse, firstMouse;

    public SerpentMouse()
    {
        newMouse = Mouse.GetState();
        newMousePos = new Vector2(newMouse.Position.X, newMouse.Position.Y);

        oldMouse = newMouse;
        oldMousePos = newMousePos;
        firstMouse = newMouse;
        firstMousePos = newMousePos;

        GetMouseAndAdjust();
    }

    #region VectorHelper

    public MouseState First
    {
        get { return firstMouse; }
    }

    public MouseState New
    {
        get { return newMouse; }
    }

    public MouseState Old
    {
        get { return oldMouse; }
    }

    #endregion

    public void Update()
    {
        oldMouse = newMouse;
        oldMousePos = newMousePos;

        GetMouseAndAdjust();

        if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
        {
            firstMouse = newMouse;
            firstMousePos = newMousePos = GetScreenPos(firstMouse);
        }
    }

    public float GetDistanceFromClick()
    {
        return VectorHelper.GetDistance(newMousePos, firstMousePos);
    }

    public void GetMouseAndAdjust()
    {
        newMouse = Mouse.GetState();
        newMousePos = GetScreenPos(newMouse);
    }

    public int GetMouseWheelChange()
    {
        return newMouse.ScrollWheelValue - oldMouse.ScrollWheelValue;
    }

    public Vector2 GetScreenPos(MouseState MOUSE)
    {
        Vector2 tempVec = new Vector2(MOUSE.Position.X, MOUSE.Position.Y);

        return tempVec;
    }

    public bool LeftClick()
    {
        if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= GraphicsConfig.SCREEN_WIDTH && newMouse.Position.Y >= 0 && newMouse.Position.Y <= GraphicsConfig.SCREEN_HEIGHT)
        {
            return true;
        }

        return false;
    }

    public bool LeftClickHold()
    {
        bool holding = false;

        if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= GraphicsConfig.SCREEN_WIDTH && newMouse.Position.Y >= 0 && newMouse.Position.Y <= GraphicsConfig.SCREEN_HEIGHT)
        {
            holding = true;

            if (Math.Abs(newMouse.Position.X - firstMouse.Position.X) > 8 || Math.Abs(newMouse.Position.Y - firstMouse.Position.Y) > 8)
            {
                LeftClickDrag = true;
            }
        }

        return holding;
    }

    public bool MiddleClickHold()
    {
        if (newMouse.MiddleButton == ButtonState.Pressed && oldMouse.MiddleButton == ButtonState.Pressed)
        {
            return true;
        }

        return false;
    }

    public Vector2 GetNewPosition()
    {
        return newMousePos;
    }

    public bool LeftClickRelease()
    {
        if (newMouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed)
        {
            LeftClickDrag = false;
            return true;
        }

        return false;
    }

    public bool RightClick()
    {
        if (newMouse.RightButton == ButtonState.Pressed && oldMouse.RightButton != ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= GraphicsConfig.SCREEN_WIDTH && newMouse.Position.Y >= 0 && newMouse.Position.Y <= GraphicsConfig.SCREEN_HEIGHT)
        {
            return true;
        }

        return false;
    }

    public bool RightClickHold()
    {
        bool holding = false;

        if (newMouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= GraphicsConfig.SCREEN_WIDTH && newMouse.Position.Y >= 0 && newMouse.Position.Y <= GraphicsConfig.SCREEN_HEIGHT)
        {
            holding = true;

            if (Math.Abs(newMouse.Position.X - firstMouse.Position.X) > 8 || Math.Abs(newMouse.Position.Y - firstMouse.Position.Y) > 8)
            {
                RightClickDrag = true;
            }
        }

        return holding;
    }

    public bool RightClickRelease()
    {
        if (newMouse.RightButton == ButtonState.Released && oldMouse.RightButton == ButtonState.Pressed)
        {
            RightClickDrag = false;
            return true;
        }

        return false;
    }
}

