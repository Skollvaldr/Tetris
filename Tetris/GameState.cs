using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Tetris
{
    public class GameState
    {
        protected SpriteFont font;
        private Texture2D background;
        protected static InputHandler inputHandler;

        public GameState(ContentManager Content)
        {
            // Laad een achtergrond en een spritefont.
            background = Content.Load<Texture2D>("background");
            font = Content.Load<SpriteFont>("Score");
            inputHandler = new InputHandler();
        }

        public void Update()
        {
            inputHandler.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Vector2(), Color.White);
        }

        public static InputHandler InputHandler
        {
            get { return inputHandler; }
        }
    }

    class GameStart : GameState
    {
        public GameStart(ContentManager Content) : base(Content) { }
        public void Update(ContentManager Content)
        {
            inputHandler.Update();

            // Zorgt ervoor dat het spel start als je op enter klikt.
            if (inputHandler.KeyPressed(Keys.Enter))
            {
                Game1.Gamestate = Game1.GameState.gameWorld;
                Game1.Gameworld = new GameWorld(Content);
            }
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // Laat de speler weten dat het spel gestart kan worden door op enter te klikken.
            spriteBatch.DrawString(font, "Press enter to start", new Vector2(56, 320), Color.Black, 0, new Vector2(), 2f, 0, 0);
        }
    }

    public class GameOver : GameState
    {
        SoundEffect gameOver;

        public GameOver(ContentManager Content) : base(Content) 
        { 
            // Laadt en speelt het soundeffect voor als je gameover gaat.
            gameOver = Content.Load<SoundEffect>("game_over");
            gameOver.Play();
        }
        public void Update(ContentManager Content)
        {
            inputHandler.Update();
            
            // Als je op enter klikt terwijl je gameover bent dan restart je het spel.
            if (inputHandler.KeyPressed(Keys.Enter))
            {
                Game1.Gamestate = Game1.GameState.gameWorld;
                Game1.Gameworld = new GameWorld(Content);
            }
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // laat de speler weten dat de game opnieuw kan worden gespeeld door op enter te klikken.
            spriteBatch.DrawString(font, "Game over!", new Vector2(128, 288), Color.Black, 0, new Vector2(), 2f, 0, 0);
            spriteBatch.DrawString(font, "Press Enter to restart", new Vector2(8, 352), Color.Black, 0, new Vector2(), 2f, 0, 0);
        }
    }
}
