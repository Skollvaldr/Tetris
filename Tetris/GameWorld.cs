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

        public GameWorld(ContentManager Content)
        {
            grid = new Grid(Content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            grid.Draw(spriteBatch);
        }
    }
}
