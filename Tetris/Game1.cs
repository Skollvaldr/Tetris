using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private InputHandler inputHandler;
        private GameStart gameStart;
        private static GameWorld gameWorld;
        private static GameOver gameOver;
        public enum GameState { gameStart, gameWorld, gameOver };
        private static GameState gameState;


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
            // Hierdoor begint de game in de gamestart en kunnen spritebatch en inputhandler gebruikt worden.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            inputHandler = new InputHandler();
            gameStart = new GameStart(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            // Deze if-statements zorgen ervoor dat op elk moment de correcte gamestate update.
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
            // Deze if-statements zorgen ervoor dat op elk moment de correcte gamestate getekent wordt.
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

        // Deze 3 methode zorgen ervoor dat de gamestates verandert kunnen worden vanuit elke class.
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

        public static GameOver Gameover
        {
            get { return gameOver; }
            set { gameOver = value; }
        }
    }
}
