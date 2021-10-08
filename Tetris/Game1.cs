using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        InputHandler inputHandler;
        GameWorld gameWorld;
        public enum GameState { start, playing, gameover };
        public static GameState gameState;
        Texture2D background;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 768;
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        public static GameState Gamestate
        {
            get { return gameState;  }
            set { gameState = value; }
        }
        
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            inputHandler = new InputHandler();
            gameWorld = new GameWorld(Content);
            gameState = GameState.start;
            background = Content.Load<Texture2D>("background");
        }

        protected override void Update(GameTime gameTime)
        {
            inputHandler.Update();
            if (inputHandler.KeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Vector2(), Color.White);
            gameWorld.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
