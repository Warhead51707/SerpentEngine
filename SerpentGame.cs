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

        // Properties
        public static int FPS { get; private set; }
        public static GameTime GameTime { get; private set; }
        public static float DeltaTime { get; private set; }

        private int frameCount;
        private float elapsedTime;

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
           Input.Initialize();

           SerpentEngine.Draw.Initialize(GraphicsDevice);

           ImGuiManager.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            // FPS Calculation
            frameCount++;
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime >= 1)
            {
                FPS = (int)(frameCount / elapsedTime);
                frameCount = 0;
                elapsedTime = 0;
            }
            //

            GameTime = gameTime;
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (IsActive) Input.Update();
                
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
