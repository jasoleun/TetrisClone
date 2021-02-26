using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace TetrisClone
{
    class Playfield
    {
        private int[,] array = new int[10, 40];

        private BlockRotator blockRotator = new BlockRotator();

        private BlockType blockType = new BlockType();
        private Texture2D blockTexture;
        private Texture2D playfieldTexture;

        private KeyboardState oldState;

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

        private int[,] currentTetramino;

        public Playfield()
        {

        }

        public void Load(ContentManager Content)
        {
            blockTexture = Content.Load<Texture2D>("blockTexture");
            playfieldTexture = Content.Load<Texture2D>("playfieldTexture");

            currentTetramino = blockType.ZBlock;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();
            if (oldState.IsKeyUp(Keys.A) && newState.IsKeyDown(Keys.A))
            {
                currentTetramino = blockRotator.Rotate(currentTetramino, "left");
            }
            else if (oldState.IsKeyUp(Keys.D) && newState.IsKeyDown(Keys.D))
            {
                currentTetramino = blockRotator.Rotate(currentTetramino, "right");
            }
            oldState = newState;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(playfieldTexture, new Vector2(0, 0), Color.White);
            for (int row = 0; row < array.GetLength(0); row++)
            {
                for (int col = 0; col < array.GetLength(1); col++)
                {
                    if (array[row, col] != 0)
                    {
                        _spriteBatch.Draw(blockTexture, new Vector2(row * 16, col * 16), tetraminoColors[array[row, col]]);
                    }
                }
            }
            for (int row = 0; row < currentTetramino.GetLength(0); row++)
            {
                for (int col = 0; col < currentTetramino.GetLength(1); col++)
                {
                    if (currentTetramino[row, col] != 0)
                    {
                        _spriteBatch.Draw(blockTexture, new Vector2(col * 16, row * 16), tetraminoColors[currentTetramino[row, col]]);
                    }
                }
            }
            _spriteBatch.End();
        }
    }
}
