﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TetrisClone
{
    class Playfield
    {
        private int[,] array = new int[10, 40];

        private Texture2D blockTexture;
        private Texture2D playfieldTexture;

        double fallTimer = 0;
        int fallRate = 1000;
        double controlCooldown = 0;
        bool landed = false;

        private SpriteFont font;

        private KeyboardState oldState;

        private Tetramino currentTetramino;

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

        public Playfield() { }

        public void Load(ContentManager Content)
        {
            blockTexture = Content.Load<Texture2D>("blockTexture");
            playfieldTexture = Content.Load<Texture2D>("playfieldTexture");

            font = Content.Load<SpriteFont>("Arial");

            currentTetramino = new Tetramino(1);
        }

        public void Update(GameTime gameTime)
        {
            fallRate = 1000;
            fallTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            controlCooldown += gameTime.ElapsedGameTime.TotalMilliseconds;
            KeyboardState newState = Keyboard.GetState();

            if (oldState.IsKeyUp(Keys.W) && newState.IsKeyDown(Keys.W))
            {
                currentTetramino.Rotate("left");
            }
            else if (oldState.IsKeyUp(Keys.S) && newState.IsKeyDown(Keys.S))
            {
                currentTetramino.Rotate("right");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) && controlCooldown >= 150)
            {
                currentTetramino.position.X -= 1;
                controlCooldown = 0;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) && controlCooldown >= 150)
            {
                currentTetramino.position.X += 1;
                controlCooldown = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                fallRate = 100;
            }

            oldState = newState;
            if (fallTimer >= fallRate)
            {
                currentTetramino.position.Y += 1;
                fallTimer = 0;
            }

            for (int col = 0; col < currentTetramino.BlockShape.GetLength(1); col++)
            {
                for (int row = 0; row < currentTetramino.BlockShape.GetLength(0); row++)
                {
                    if (currentTetramino.BlockShape[col, row] != 0 && row + currentTetramino.position.X < 0)
                    {
                        currentTetramino.position.X += 1;
                    }

                    if (currentTetramino.BlockShape[col, row] != 0 && row + currentTetramino.position.X > 9)
                    {
                        currentTetramino.position.X -= 1;
                    }

                    if (currentTetramino.BlockShape[col, row] != 0 && col + currentTetramino.position.Y > 39)
                    {
                        currentTetramino.position.Y -= 1;
                        landed = true;
                    }
                }
            }

            for (int row = 0; row < currentTetramino.BlockShape.GetLength(0); row++)
            {
                for (int col = 0; col < currentTetramino.BlockShape.GetLength(1); col++)
                {
                    if (currentTetramino.BlockShape[row, col] != 0 && array[(int)currentTetramino.position.X + col, (int)currentTetramino.position.Y + row] != 0)
                    {
                        currentTetramino.position.Y -= 1;
                        landed = true;
                    }
                }
            }

            if (landed)
            {
                for (int col = 0; col < currentTetramino.BlockShape.GetLength(1); col++)
                {
                    for (int row = 0; row < currentTetramino.BlockShape.GetLength(0); row++)
                    {
                        if (currentTetramino.BlockShape[col, row] != 0)
                        {
                            array.SetValue(currentTetramino.BlockShape[col, row], row + (int)currentTetramino.position.X, col + (int)currentTetramino.position.Y);
                        }
                    }
                }
                landed = false;
                currentTetramino.position.Y = 0;
            }
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(playfieldTexture, new Vector2(0, 0), Color.White);

            for (int row = 0; row < array.GetLength(0); row++)
            {
                for (int col = 0; col < array.GetLength(1); col++)
                {
                    _spriteBatch.Draw(blockTexture, new Vector2(row * 16, col * 16), tetraminoColors[array[row, col]]);
                }
            }

            for (int row = 0; row < currentTetramino.BlockShape.GetLength(0); row++)
            {
                for (int col = 0; col < currentTetramino.BlockShape.GetLength(1); col++)
                {
                    if (currentTetramino.BlockShape[row, col] != 0)
                    {
                        _spriteBatch.Draw(blockTexture, new Vector2((col + currentTetramino.position.X) * 16, (row + currentTetramino.position.Y) * 16), tetraminoColors[currentTetramino.BlockShape[row, col]]);
                    }
                }
            }
            _spriteBatch.End();
        }
    }
}
