using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    public class GameState
    {
        protected SpriteFont font;
        Texture2D background;
        protected Grid grid;
        protected InputHandler inputHandler;

        public GameState(ContentManager Content)
        {
            background = Content.Load<Texture2D>("background");
            font = Content.Load<SpriteFont>("Score");
            grid = new Grid(Content);
            inputHandler = new InputHandler();
        }

        public void Drawing(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Vector2(), Color.White);
            grid.Draw(spriteBatch);
        }
    }

    class GameStart : GameState
    {
        public GameStart(ContentManager Content) : base(Content) { }
        public void Update(ContentManager Content)
        {
            inputHandler.Update();
            if (inputHandler.KeyPressed(Keys.Space))
            {
                Game1.Gamestate = Game1.GameState.gameWorld;
                Game1.Gameworld = new GameWorld(Content);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Drawing(spriteBatch);
            spriteBatch.DrawString(font, "Press space\r\nto start", new Vector2(320, 350), Color.Black, 0, new Vector2(), 2f, 0, 0);
        }
    }

    class GameOver : GameState
    {
        public GameOver(ContentManager Content) : base(Content) { }
        public void Update(ContentManager Content)
        {
            inputHandler.Update();
            if (inputHandler.KeyPressed(Keys.Space))
            {
                Game1.Gamestate = Game1.GameState.gameWorld;
                Game1.Gameworld = new GameWorld(Content);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Drawing(spriteBatch);
            spriteBatch.DrawString(font, "Game over\r\n \r\nPress space to restart", new Vector2(320, 350), Color.Black, 0, new Vector2(), 2f, 0, 0);
        }
    }
}
