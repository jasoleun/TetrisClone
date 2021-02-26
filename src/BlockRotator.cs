namespace TetrisClone
{
    class BlockRotator
    {
        public BlockRotator()
        {

        }

        public int[,] Rotate(int[,] currentTetramino, string rotationDirection)
        {
            int[,] newTetramino = new int[currentTetramino.GetLength(0), currentTetramino.GetLength(1)];
            if (rotationDirection == "left")
            {
                for (int row = 0; row < currentTetramino.GetLength(0); ++row)
                {
                    for (int col = 0; col < currentTetramino.GetLength(1); ++col)
                    {
                        newTetramino[row, col] = currentTetramino[col, currentTetramino.GetLength(0) - row - 1];
                    }
                }
            }
            else if (rotationDirection == "right")
            {
                for (int row = 0; row < currentTetramino.GetLength(0); ++row)
                {
                    for (int col = 0; col < currentTetramino.GetLength(1); ++col)
                    {
                        newTetramino[row, col] = currentTetramino[currentTetramino.GetLength(0) - col - 1, row];
                    }
                }
            }
            return (newTetramino);
        }
    }
}
