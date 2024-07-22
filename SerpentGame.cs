using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SerpentEngine
{
    public class SerpentGame : Game
    {
        // References
        public static SerpentGame Instance { get; private set; }
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static SceneManager SceneManager { get; private set; } = new SceneManager();

        // Properties
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
           SerpentEngine.Draw.Initialize(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            SceneManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SceneManager.Draw();

            base.Draw(gameTime);
        }
    }
}
