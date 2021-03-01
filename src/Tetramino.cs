using Microsoft.Xna.Framework;

namespace TetrisClone
{
    class Tetramino
    {
        private int[,] IBlock = new int[4, 4]
        {
            { 0, 0, 0, 0 },
            { 1, 1, 1, 1 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };

        private int[,] JBlock = new int[3, 3]
        {
            { 2, 0, 0 },
            { 2, 2, 2 },
            { 0, 0, 0 }
        };

        private int[,] LBlock = new int[3, 3]
        {
            { 0, 0, 3 },
            { 3, 3, 3 },
            { 0, 0, 0 },
        };

        private int[,] OBlock = new int[2, 2]
        {
            { 4, 4 },
            { 4, 4 }
        };

        public int[,] SBlock = new int[3, 3]
        {
            { 0, 5, 5 },
            { 5, 5, 0 },
            { 0, 0, 0 }
        };

        private int[,] TBlock = new int[3, 3]
        {
            { 0, 6, 0 },
            { 6, 6, 6 },
            { 0, 0, 0 }
        };

        private int[,] ZBlock = new int[3, 3]
        {
            { 7, 7, 0 },
            { 0, 7, 7 },
            { 0, 0, 0 }
        };

        public int[,] BlockShape;
        public Vector2 position;

        public Tetramino(int blockType)
        {
            switch (blockType)
            {
                case 0:
                    BlockShape = IBlock;
                    break;
                case 1:
                    BlockShape = JBlock;
                    break;
                case 2:
                    BlockShape = LBlock;
                    break;
                case 3:
                    BlockShape = OBlock;
                    break;
                case 4:
                    BlockShape = SBlock;
                    break;
                case 5:
                    BlockShape = TBlock;
                    break;
                case 6:
                    BlockShape = ZBlock;
                    break;
            }
        }

        public void Rotate(string rotationDirection)
        {
            int[,] newBlockShape = new int[BlockShape.GetLength(0), BlockShape.GetLength(1)];
            
            for (int row = 0; row < BlockShape.GetLength(0); ++row)
            {
                for (int col = 0; col < BlockShape.GetLength(1); ++col)
                {
                    if (rotationDirection == "left")
                    {
                        newBlockShape[row, col] = BlockShape[col, BlockShape.GetLength(0) - row - 1];
                    }
                    else if (rotationDirection == "right")
                    {
                        newBlockShape[row, col] = BlockShape[BlockShape.GetLength(0) - col - 1, row];
                    }
                }
            }
            BlockShape = newBlockShape;
        }

        public void ChangeTetramino(int blockType)
        {
            switch (blockType)
            {
                case 0:
                    BlockShape = IBlock;
                    break;
                case 1:
                    BlockShape = JBlock;
                    break;
                case 2:
                    BlockShape = LBlock;
                    break;
                case 3:
                    BlockShape = OBlock;
                    break;
                case 4:
                    BlockShape = SBlock;
                    break;
                case 5:
                    BlockShape = TBlock;
                    break;
                case 6:
                    BlockShape = ZBlock;
                    break;
            }
        }
    }
}
