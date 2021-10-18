using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Linq;

namespace Tetris
{
    public class GameWorld : GameState
    {
        public static int score;
        Straight straight;
        Cube cube;
        Right right;
        Left left;
        Middle middle;
        RightZig rightZig;
        LeftZig leftZig;
        Random random;
        List<Block> nextBlock;
        Block currentBlock;

        public GameWorld(ContentManager Content) : base(Content)
        {
            score = 0;
            nextBlock = new List<Block>();
            random = new Random();
            GetNewBlock(Content);
        }

        public void Update(GameTime gameTime, ContentManager Content)
        {
            grid.Update();
            currentBlock.Update(gameTime);
            if (currentBlock.pos.Y > 768)
            {
                GetNewBlock(Content);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Drawing(spriteBatch);
            currentBlock.Draw(spriteBatch);
            spriteBatch.DrawString(font, "" + score, new Vector2(352, 0), Color.Black);
            spriteBatch.DrawString(font, "" + nextBlock.Count, new Vector2(352, 64), Color.Black);
        }
        private void ShuffleList(List<Block> list)
        {
            list = list.OrderBy(x => random.Next()).ToList();
        }

        private List<Block> Blocks(ContentManager Content)
        {
            List<Block> blocks = new List<Block>();
            blocks.Add(straight = new Straight(Content));
            blocks.Add(cube = new Cube(Content));
            blocks.Add(right = new Right(Content));
            blocks.Add(left = new Left(Content));
            blocks.Add(middle = new Middle(Content));
            blocks.Add(rightZig = new RightZig(Content));
            blocks.Add(leftZig = new LeftZig(Content));
            return blocks;
        }

        public void GetNewBlock(ContentManager Content)
        {
            if (nextBlock.Count <= 7)
            {
                List<Block> blocks = Blocks(Content);
                ShuffleList(blocks);
                nextBlock.AddRange(blocks);
            }
            currentBlock = nextBlock[0];
            nextBlock.RemoveAt(0);
        }
    }
}
