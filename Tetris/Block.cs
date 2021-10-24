using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Tetris
{
    public class Block
    {
        private Texture2D block;
        public bool[,] shape;
        protected Color shapeColor;
        public Point pos;
        private Point spd;
        private Point move;
        SoundEffect blockPlaced;

        public Block(ContentManager Content)
        {
            block = Content.Load<Texture2D>("block");
            blockPlaced = Content.Load<SoundEffect>("block_place");
            spd = new Point(0, 32);
            move = new Point(32, 0);
        }

        public void Update(GameTime gameTime, ContentManager Content)
        {
            // Het if-statement regelt de snelheid en verplaatsing van het blok naar beneden. Als levelUp groter wordt gaat het blok sneller.
            // Als het blok door deze verplaatsing een collision heeft dan verplaatst hij terug naar zijn originele positie en spawnt er een nieuw blok.
            if ((int)(gameTime.TotalGameTime.TotalMilliseconds % (1000d / (double)Game1.Gameworld.level)) == 0)
            {
                pos += spd;
                if (Collision())
                {
                    pos -= spd;
                    ColorGrid();
                    Game1.Gameworld.GetNewBlock(Content);
                    blockPlaced.Play();
                }
            }

            // zorgt ervoor dat de methode voor het draaien van het blok wordt aangeroepen en dat hij naar het midden toe beweegt als hij hierdoor buiten het grid zou komen.
            // Als door deze draaiing een collision zou plaatsvinden dan draait het blok weer terug waardoor het dus eigenlijk niet heeft gedraait.
            if (GameState.InputHandler.KeyPressed(Keys.Up))
            { 
                shape = RotateMatrix(shape, shape.GetLength(0));
                while (OutOfBounds())
                {
                    if (pos.X < 0)
                        pos += move;
                    else
                        pos -= move;
                }
                if (Collision())
                    shape = InvertedRotateMatrix(shape, shape.GetLength(0));
            }

            // Als er op spatie gedrukt wordt zal het blok net zo lang naar beneden gaan totdat er een collision plaats vindt. Zodra dit gebeurt beweegt het blok een blokje omhoog.
            if (GameState.InputHandler.KeyPressed(Keys.Space) )
            {
                while (!Collision())
                    pos += spd;
                pos -= spd;
                Game1.Gameworld.score += 5;
                ColorGrid();
                Game1.Gameworld.GetNewBlock(Content);
                blockPlaced.Play();

            }

            // Zorgt ervoor dat het blok naar links beweegt als er op pijltje naar links wordt gedrukt behalve als hij hierdoor buiten de grid komt. 
            else if (GameState.InputHandler.KeyPressed(Keys.Left))
            {
                pos -= move;
                if (OutOfBounds())
                    pos += move;
                else if (Collision())
                    pos += move;
            }

            // Zorgt ervoor dat het blok naar rechts beweegt als er op pijltje naar rechts wordt gedrukt behalve als hij hierdoor buiten de grid komt.
            else if (GameState.InputHandler.KeyPressed(Keys.Right))
            {
                pos += move;
                if (OutOfBounds())
                    pos -= move;
                else if (Collision())
                    pos -= move;
            }

            //beweegt het blok een blokje naar beneden als er op pijltje naar beneden wordt gedrukt tenzij het blok hierdoor collide.
            else if (GameState.InputHandler.KeyPressed(Keys.Down))
            {
                pos += spd;
                if (Collision())
                    pos -= spd;
            }
        }

        // Tekent de array op de goede plek op het beeld waarbij alle blokjes de goede kleur en positie hebben.
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < shape.GetLength(0); i++)
                for (int j = 0; j < shape.GetLength(1); j++)
                    if (shape[i, j])
                        spriteBatch.Draw(block, new Vector2(i * 32 + pos.X, j * 32 + pos.Y), shapeColor);
        }

        // Tekent het volgende blok
        public void Draw2nd(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < shape.GetLength(0); i++)
                for (int j = 0; j < shape.GetLength(1); j++)
                    if (shape[i, j])
                        spriteBatch.Draw(block, new Vector2(i * 32 + 352, j * 32 + 128), shapeColor);
        }

        // Zorgt ervoor dat de matrix van het blokje op een correcte manier wordt gedraait als deze methode wordt aangeroepen.
        private bool[,] RotateMatrix(bool[,] matrix, int n)
        {
            bool[,] ret = new bool[n,n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    ret[i, j] = matrix[n - j - 1, i];
            return ret;
        }

        // Zorgt ervoor dat de matrix van het blokje op een correcte manier wordt gedraait als deze methode wordt aangeroepen.
        private bool[,] InvertedRotateMatrix(bool[,] matrix, int n)
        {
            bool[,] ret = new bool[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    ret[i, j] = matrix[j, n - i - 1];
            return ret;
        }

        // Berekent of het blok buiten de grid is.
        public bool OutOfBounds()
        {
            if (pos.X < 0)
            {
                for (int i = 0; i < shape.GetLength(0); i++)
                {
                    for (int j = 0; j < shape.GetLength(1); j++)
                        if (shape[i, j])
                            if (pos.X < 0 - i * 32)
                                return true;
                }
            }
            else
            {
                for (int i = shape.GetLength(0) - 1; i >= 0; i--)
                {
                    for (int j = shape.GetLength(1) - 1; j >= 0; j--)
                        if (shape[i, j])
                            if (pos.X > 320 - shape.GetLength(1) * 32 + (shape.GetLength(0) - i - 1) * 32)
                                return true;
                }
            }
            return false;
        }

        // Kijkt of het blok een collision heeft met een ander voorwerp.
        public bool Collision()
        {
            for (int i = 0; i < shape.GetLength(0); i++)
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    int GridX = (int)pos.X / 32 + i;
                    int GridY = (int)pos.Y / 32 + j;
                    if (GridY < 0)
                        GridY = 0;
                    if (shape[i, j] && Grid.Filled[GridX, GridY])
                        return true;
                }
            return false;
        }

        // Geeft aan of een specifiek blokje in de grid een klaur heeft.
        public void ColorGrid()
        {
            for (int i = 0; i < shape.GetLength(0); i++)
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    int GridX = (int)pos.X / 32 + i;
                    int GridY = (int)pos.Y / 32 + j;
                    if (GridY < 0)
                        GridY = 0;
                    if (shape[i, j])
                        Game1.Gameworld.grid.Gridcolor(GridX, GridY, ShapeColor);
                }
        }

        public Color ShapeColor
        {
            get { return shapeColor; }
        }
    }

    // De volgende classes geven van elk blok de vorm weer in een array, de kleur en de startpositie.
    class Straight : Block
    {
        public Straight(ContentManager Content) : base(Content)
        {
            shape = new bool[4, 4] 
            {
                {false, false, false, false},
                {true, true, true, true},
                {false, false, false, false},
                {false, false, false, false}
            };
            shapeColor = Color.CornflowerBlue;
            pos = new Point(96, 0);
        }
    }

    class Cube : Block
    {
        public Cube(ContentManager Content) : base(Content)
        {
            shape = new bool[2, 2] 
            {
                {true, true},
                {true, true}
            };
            shapeColor = Color.Yellow;
            pos = new Point(128, 0);
        }
    }

    class Right : Block
    {
        public Right(ContentManager Content) : base(Content)
        {
            shape = new bool[3, 3] 
            {
                {true, true, false},
                {false, true, false},
                {false, true, false} 
            };
            shapeColor = Color.Orange;
            pos = new Point(128, 0);
        }
    }

    class Left : Block
    {
        public Left(ContentManager Content) : base(Content)
        {
            shape = new bool[3, 3]
            {
                {false, true, false},
                {false, true, false},
                {true, true, false} 
            };
            shapeColor = Color.Blue;
            pos = new Point(128, 0);
        }
    }

    class Middle : Block
    {
        public Middle(ContentManager Content) : base(Content)
        {
            shape = new bool[3, 3]
            {
                {false, true, false},
                {true, true, false},
                {false, true, false}
            };
            shapeColor = Color.Magenta;
            pos = new Point(128, 0);
        }
    }

    class RightZig : Block
    {
        public RightZig(ContentManager Content) : base(Content)
        {
            shape = new bool[3, 3]
            {
                {true, false, false},
                {true, true, false},
                {false, true, false}
            };
            shapeColor = Color.LimeGreen;
            pos = new Point(128, 0);
        }
    }

    class LeftZig : Block
    {
        public LeftZig(ContentManager Content) : base(Content)
        {
            shape = new bool[3, 3]
            {
                {false, true, false},
                {true, true, false},
                {true, false, false} 
            };
            shapeColor = Color.Red;
            pos = new Point(128, 0);
        }
    }
}
