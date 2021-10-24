using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Tetris
{
    public class Grid
    {
        private Texture2D block;
        private Color[,] grid;
        private static bool[,] filled;
        public bool[] fullRow;
        private int scoreMultiplier;
        SoundEffect fullRowSound;

        public Grid(ContentManager Content)
        {
            // Initialiseerd een aantal variabelen en vult het grid
            block = Content.Load<Texture2D>("block");
            fullRowSound = Content.Load<SoundEffect>("clear_row");
            grid = new Color[10, 24];
            filled = new bool[10, 25];
            fullRow = new bool[24];
            for (int j = 0; j < 24; j++)
                fullRow[j] = false;
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 24; j++)
                { 
                    grid[i, j] = Color.White;
                    filled[i, j] = false;
                }
            for (int i = 0; i < 10; i++)
                filled[i, 24] = true;
            scoreMultiplier = 0;
        }

        public void Update()
        {
            // Verandert de waardes in de array filled afhankelijk van de kleur.
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 24; j++)
                {
                    if (grid[i, j] == Color.White)
                        filled[i, j] = false;
                    else
                        filled[i, j] = true;
                }

            // Verandert de score van de speler als scoreMultiplier verandert.
            if (scoreMultiplier != 0)
            {
                Game1.Gameworld.score += (int)((double)10 * Math.Pow(1.5, (double)scoreMultiplier));
                scoreMultiplier = 0;
            }

            // Roept ClearRow aan.
            ClearRow();
        }

        // Tekent de grid op de correcte plek met de correcte kleur.
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 24; j++)
                    spriteBatch.Draw(block, new Vector2(i * 32, j * 32), grid[i,j]);
        }

        public void Gridcolor(int x, int y, Color color)
        {
            grid[x, y] = color;
        }

        // Checkt of er een rij in de grid vol is
        public void FullRow()
        {
            for (int j = 23; j > 0; j--)
                for (int i = 0; i <= 10; i++)
                {
                    if (i == 10)
                    {
                        fullRow[j] = true;
                    }
                    else if (!filled[i, j])
                        break;
                }
        }

        // Als er een rij in de grid vol is dan wordt deze verwijdert en er speelt een geluidje en scoreMultiplier verandert.
        public void ClearRow()
        {
            FullRow();
            for (int k = 0; k < 24; k++)
                if (fullRow[k])
                {
                    for (int j = k; j > 0; j--)
                        for (int i = 0; i < 10; i++)
                            grid[i, j] = grid[i, j - 1];
                    fullRow[k] = false;
                    fullRowSound.Play();
                    scoreMultiplier++;
                }
        }
    
        // Zorgt ervoor dat andere classes de waardes van de grid filled kunnen zien.
        public static bool[,] Filled
        {
            get { return filled; }
        }
    }
}
