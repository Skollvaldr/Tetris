using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Tetris
{
    public class GameWorld : GameState
    {
        public int score;
        private int bar;
        private Straight straight;
        private Cube cube;
        private Right right;
        private Left left;
        private Middle middle;
        private RightZig rightZig;
        private LeftZig leftZig;
        private Random random;
        private List<Block> nextBlock;
        private Block currentBlock;
        public int level;
        public Grid grid;
        SoundEffect levelUpSound;
        public int minLevelUp;


        public GameWorld(ContentManager Content) : base(Content)
        {
            bar = 1;
            score = 0;
            level = 1;
            nextBlock = new List<Block>();
            random = new Random();
            grid = new Grid(Content);
            GetNewBlock(Content);
            levelUpSound = Content.Load<SoundEffect>("lvl_up");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Content.Load<Song>("loop"));
        }

        // Zorgt ervoor dat de correcte classes worden ge-update als de game bezig is en dat het level aangeroepen wordt als de speler een bepaald aantal score behaald.
        public void Update(GameTime gameTime, ContentManager Content) 
        {
            base.Update();
            currentBlock.Update(gameTime, Content);
            grid.Update();
            if (score / 150 == bar && score != 0)
                Level();
        }

        // Verantwoordelijk voor het tekenen van de score, het level en de goede objects
        public new void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            grid.Draw(spriteBatch);
            currentBlock.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(352, 0), Color.Black);
            spriteBatch.DrawString(font, "Level: " + level, new Vector2(352, 64), Color.Black);
            nextBlock[0].Draw2nd(spriteBatch);
        }

        // Schudt de inhoud van de list door elkaar.
        public List<Block> ShuffleList(List<Block> list)
        {
            list = list.OrderBy(x => random.Next()).ToList();
            return list;
        }

        // Returnt een lijst van alle blokken
        public List<Block> Blocks(ContentManager Content)
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
            // Voegt een nieuwe list met blokken toe aan de komende blokken op het moment dat er nog maar een paar blokken over zijn.
            if (nextBlock.Count <= 7)
            {
                List<Block> blocks = Blocks(Content);
                blocks = ShuffleList(blocks);
                nextBlock.AddRange(blocks);
            }
            currentBlock = nextBlock[0];
            nextBlock.RemoveAt(0);
            
            // Als er geen nieuw blok meer kan spawnen zonder gelijk te colliden gaat de speler game over.
            if (currentBlock.Collision())
            {
                Game1.Gamestate = Game1.GameState.gameOver;
                Game1.Gameover = new GameOver(Content);
                MediaPlayer.Stop();
            }
        }

        // Zorgt ervoor dat het level en de waarde van de snelheid van de blokken wordt verandert als Level wordt aangeroepen.
        public void Level()
        {
            level++;
            levelUpSound.Play();
            if (level > 20)
                level = 20;
            bar++;
        }
    }
}
