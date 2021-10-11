using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Tetris
{
    class GameWorld
    {
        /*
        Red
        Blue
        Green
        Yellow
        Orange
        Cornflower
        Magenta
        */

        Grid grid;
        public static int score;
        SpriteFont font;

        public GameWorld(ContentManager Content)
        {
            grid = new Grid(Content);
            font =  Content.Load<SpriteFont>("Score");
            score = 0;
        }

        public void Update()
        {
            grid.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            grid.Draw(spriteBatch);
            spriteBatch.DrawString(font, "" + score, new Vector2(352, 0), Color.Black);
        }
    }
}
