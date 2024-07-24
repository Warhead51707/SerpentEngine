﻿using Cheesed;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace SerpentEngine
{
    public class SerpentGame : Game
    {
        // References
        public static SerpentGame Instance { get; private set; }
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static SceneManager SceneManager { get; private set; } = new SceneManager();
        public static ImGuiManager ImGuiManager { get; private set; } = new ImGuiManager();

        public static SerpentKeyboard Keyboard;


        public static SerpentMouse Mouse;


        // Properties
        public static GameTime GameTime { get; private set; }
        public static float DeltaTime { get; private set; }

        public SerpentGame(string windowTitle)
        {
            Instance = this;

            Window.Title = windowTitle;

            Graphics = new GraphicsDeviceManager(this);
            IsFixedTimeStep = false;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
           Keyboard = new SerpentKeyboard();
           Mouse = new SerpentMouse();

            SerpentEngine.Draw.Initialize(GraphicsDevice);

           ImGuiManager.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            GameTime = gameTime;
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Keyboard.Update();
            Mouse.Update();


            Keyboard.UpdateOld();
            Mouse.UpdateOld();

            SceneManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SceneManager.Draw();

#if DEBUG
            ImGuiManager.Draw();
#endif

            base.Draw(gameTime);
        }
    }
}
