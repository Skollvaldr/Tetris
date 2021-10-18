using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Tetris
{
    public class Grid
    {
        Texture2D block;
        public Color[,] grid;
        public bool[,] filled;
        public bool[] fullRow;
        int scoreMultiplier;

        public Grid(ContentManager Content)
        {
            block = Content.Load<Texture2D>("block");
            grid = new Color[10, 24];
            filled = new bool[10, 24];
            fullRow = new bool[24];
            for (int j = 0; j < 24; j++)
                fullRow[j] = false;
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 24; j++)
                { 
                    grid[i, j] = Color.White;
                    filled[i, j] = false;
                }
            scoreMultiplier = 0;
        }

        public void Update()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 24; j++)
                {
                    if (grid[i, j] == Color.White)
                        filled[i, j] = false;
                    else
                        filled[i, j] = true;
                }
            if (scoreMultiplier != 0)
            {
                GameWorld.score += (int)((double)10 * Math.Pow(1.5, (double)scoreMultiplier));
                scoreMultiplier = 0;
            }
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

        public void FullRow()
        {
            for (int j = 23; j >= 0; j--)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (!filled[i, j])
                        break;
                    else
                        fullRow[j] = true;
                }
            }

            for (int k = 0; k <= 23; k++)
                {
                if (fullRow[k])
                {
                    for (int j = 23; j >= 0; j--)
                        for (int i = 0; i <= 9; i++)
                            grid[i, j] = grid[i, j - 1];
                    scoreMultiplier++;
                    fullRow[k] = false;
                }
            }
        }
    }
}
