using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    class Block
    {
        Texture2D block;
        InputHandler inputHandler;
        public bool[,] shape;
        public Color shapeColor;
        public Vector2 pos;
        Vector2 spd;
        Vector2 move;

        public Block(ContentManager Content)
        {
            inputHandler = new InputHandler();
            block = Content.Load<Texture2D>("block");
            spd = new Vector2(0, 32);
            move = new Vector2(32, 0);
        }

        public void Update(GameTime gameTime)
        {
            inputHandler.Update();
            if (inputHandler.MouseLeftButtonPressed())
            {
                shape = RotateMatrix(shape, shape.GetLength(0), false);
            }
            else if (inputHandler.MouseRightButtonPressed())
            {
                shape = RotateMatrix(shape, shape.GetLength(0), true);
            }
            if (inputHandler.KeyPressed(Keys.Space))
            {
                for (int i = 0; i < 10; i++)
                    pos += spd;
                GameWorld.score += 5;
            }
            else if (inputHandler.KeyPressed(Keys.A))
            {
                pos -= move;
            }
            else if (inputHandler.KeyPressed(Keys.D))
            {
                pos += move;
            }
            else if (inputHandler.KeyPressed(Keys.S))
            {
                pos += spd;
            }

            if ((int)gameTime.TotalGameTime.TotalMilliseconds % 1000 == 500)
            {
                pos += spd;
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < shape.GetLength(0); i++)
                for (int j = 0; j < shape.GetLength(1); j++)
                    if (shape[i, j] == true)
                        spriteBatch.Draw(block, new Vector2(i * 32, j * 32) + pos, shapeColor);
        }

        private bool[,] RotateMatrix(bool[,] matrix, int n, bool right)
        {
            bool[,] ret = new bool[n,n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (right)
                        ret[i, j] = matrix[n - j - 1, i];
                    else
                        ret[i, j] = matrix[j, n - i - 1];

            return ret;
        }
    }

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
            pos = new Vector2(96, -128);
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
            pos = new Vector2(128, -64);
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
            pos = new Vector2(128, -64);
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
            pos = new Vector2(128, -64);
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
            pos = new Vector2(128, -64);
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
            pos = new Vector2(128, -64);
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
            pos = new Vector2(128, -64);
        }
    }
}
