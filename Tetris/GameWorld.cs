using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Tetris
{
    class GameWorld
    {
        Grid grid;
        Straight test;
        public static int score;
        SpriteFont font;

        public GameWorld(ContentManager Content)
        {
            grid = new Grid(Content);
            test = new Straight(Content);
            font =  Content.Load<SpriteFont>("Score");
            score = 0;
        }

        public void Update(GameTime gameTime)
        {
            grid.Update();
            test.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            grid.Draw(spriteBatch);
            test.Draw(spriteBatch);
            spriteBatch.DrawString(font, "" + score, new Vector2(352, 0), Color.Black);
        }
    }
}
