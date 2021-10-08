using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Tetris
{
    class Block
    {
        Texture2D block;
        InputHandler inputHandler;

        public Block(ContentManager Content)
        {
            inputHandler = new InputHandler();
            block = Content.Load<Texture2D>("block");
        }

        public void Update(GameTime gameTime)
        {
            inputHandler.Update();
        }

    }
}
