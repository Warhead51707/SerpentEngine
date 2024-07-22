using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;
public class GraphicsConfig
{
    public static int SCREEN_WIDTH = 0;
    public static int SCREEN_HEIGHT = 0;

    public static bool VSYNC = false;

    public static int FRAMERATE = 60;

    public static bool FULLSCREEN = true;

    public static void Apply()
    {
        SerpentGame.Graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
        SerpentGame.Graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;

        SerpentGame.Graphics.SynchronizeWithVerticalRetrace = VSYNC;
        SerpentGame.Graphics.IsFullScreen = FULLSCREEN;

        SerpentGame.Graphics.ApplyChanges();
    } 
}