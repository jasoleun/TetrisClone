using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TetrisClone
{
    class Playfield
    {
        private int[,] array = new int[10, 40];
        private int[,] tempArray = new int[10, 40];
        private double elapsed;

        private Vector2 currentTetraminoPos = new Vector2(0, 0);
        private int[,] currentTetramino;
        private BlockType blockType = new BlockType();
        private Texture2D blockTexture;
        private Texture2D playfieldTexture;
        private Vector2 playfieldPosition = new Vector2(0, 0);
        private Vector2 blockPosition;

        private Color[] tetraminoColors =
        {
            Color.Transparent,
            Color.Cyan,
            Color.Blue,
            Color.Orange,
            Color.Yellow,
            Color.Green,
            Color.Purple,
            Color.Red,
        };

        public Playfield()
        {

        }

        public void Load(ContentManager Content)
        {
            blockTexture = Content.Load<Texture2D>("blockTexture");
            playfieldTexture = Content.Load<Texture2D>("playfieldTexture");
            currentTetramino = blockType.IBlock;
        }

        public void Update(GameTime gameTime)
        {
            elapsed += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (Keyboard.GetState().IsKeyDown(Keys.A) && currentTetraminoPos.X > 0)
            {
                currentTetraminoPos.X -= 1;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.D) && currentTetraminoPos.X < 6)
            {
                currentTetraminoPos.X += 1;
            }

            for (int i = 0; i < currentTetramino.GetLength(0); i++)
            {
                for (int j = 0; j < currentTetramino.GetLength(1); j++)
                {
                    tempArray[i + (int)currentTetraminoPos.X, j + (int)currentTetraminoPos.Y] = currentTetramino[i, j];
                }
            }

            if (elapsed >= 1000)
            {
                if (currentTetraminoPos.Y <= 5)
                {
                    currentTetraminoPos.Y += 1;
                }
                Array.Clear(tempArray, 0, tempArray.Length);
                elapsed = 0;
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(playfieldTexture, playfieldPosition, Color.White);
            for (int i = 0; i < tempArray.GetLength(0); i++)
            {
                for (int j = 0; j < tempArray.GetLength(1); j++)
                {
                    blockPosition.X = i * 16;
                    blockPosition.Y = j * 16;
                    if (tempArray[i, j] != 0)
                    {
                        int currentBlock = tempArray[i, j];
                        Color currentBlockColor = tetraminoColors[currentBlock];
                        _spriteBatch.Draw(blockTexture, blockPosition, currentBlockColor);
                    }
                }
            }
            _spriteBatch.End();
        }
    }
}
