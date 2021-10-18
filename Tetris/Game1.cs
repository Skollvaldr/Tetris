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
        GameStart gameStart;
        static GameWorld gameWorld;
        GameOver gameOver;
        public enum GameState { gameStart, gameWorld, gameOver };
        public static GameState gameState;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 768;
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            gameState = GameState.gameStart;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            inputHandler = new InputHandler();
            gameStart = new GameStart(Content);
            gameOver = new GameOver(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            inputHandler.Update();
            if (inputHandler.KeyDown(Keys.Escape))
                Exit();
            if (gameState == GameState.gameStart)
            {
                gameStart.Update(Content);
            }
            else if (gameState == GameState.gameWorld)
            {
                gameWorld.Update(gameTime, Content);
            }
            else if (gameState == GameState.gameOver)
            {
                gameOver.Update(Content);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            
            if (gameState == GameState.gameStart)
            {
                gameStart.Draw(_spriteBatch);
            }
            else if (gameState == GameState.gameWorld)
            {
                gameWorld.Draw(_spriteBatch);
            }
            else if (gameState == GameState.gameOver)
            {
                gameOver.Draw(_spriteBatch);
            }
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public static GameState Gamestate
        {
            get { return gameState; }
            set { gameState = value; }
        }

        public static GameWorld Gameworld
        {
            get { return gameWorld; }
            set { gameWorld = value; }
        }
    }
}
