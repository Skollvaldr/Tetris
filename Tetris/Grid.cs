using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Tetris
{
    class Grid
    {
        Texture2D block;
        public Color[,] grid;


        public Grid(ContentManager content)
        {
            block = content.Load<Texture2D>("block");
            grid = new Color[10, 24];
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 24; j++)
                    grid[i, j] = Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 24; j++)
                    spriteBatch.Draw(block, new Vector2(i*32, j*32), grid[i,j]);
        }

        public void Gridcolor(int x, int y, Color color)
        {
            grid[x, y] = color;
        }
    }
}
