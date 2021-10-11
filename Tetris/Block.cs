using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Tetris
{
    class Block
    {
        /*
        
        long

        {false, true, false, false},
        {false, true, false, false},
        {false, true, false, false},
        {false, true, false, false}

        cube

        {true, true},
        {true, true}

        right

        {false, true, false},
        {false, true, false},
        {false, true, true}

        left

        {false, true, false},
        {false, true, false},
        {true, true, false}

        middle

        {false, true, false},
        {true, true, true},
        {false, false, false}

        leftZig

        {{false, false, false},
        {true, true, false},
        {false, true, true}

        rightZig

        {false, false, false},
        {false, true, true},
        {true, true, false}

        */
        Texture2D block;
        InputHandler inputHandler;
        public bool[,] shape;
        Color shapeColor;
        Vector2 pos;
        Vector2 spd;
        Vector2 move;

        public Block(ContentManager Content)
        {
            inputHandler = new InputHandler();
            block = Content.Load<Texture2D>("block");
            spd = new Vector2(0, 32);
            move = new Vector2(32, 0);
            pos = new Vector2(0, 0);
        }

        public Block() { }

        public void Update(GameTime gameTime)
        {
            inputHandler.Update();
            if (inputHandler.MouseLeftButtonPressed())
            {

            }
            else if (inputHandler.MouseLeftButtonPressed())
            {

            }
            if (inputHandler.KeyPressed(Keys.Space))
            {

            }
            else if (inputHandler.KeyPressed(Keys.A))
            {

            }
            else if (inputHandler.KeyPressed(Keys.D))
            {
                
            }
            else if (inputHandler.KeyPressed(Keys.S))
            {

            }

            if (gameTime.TotalGameTime.TotalMilliseconds * (GameWorld.score / 500 + 1) % 500 == 0)
            {
                pos += move;
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < shape.GetLength(0); i++)
                for (int j = 0; j < shape.GetLength(1); j++)
                    if (shape[i, j] == true)
                        spriteBatch.Draw(block, new Vector2(i * 32, j * 32) + pos, shapeColor);
        }
    }

    class Straight : Block
    {
        public Straight()
        {
            shape = new bool[4,4];
        }
    }

    class Cube : Block
    {
        
    }

    class Right : Block
    {
        
    }

    class Left : Block
    {
        
    }

    class Middle : Block
    {
        
    }

    class RightZig : Block
    {
        
    }

    class LeftZig : Block
    { 
    }
}
