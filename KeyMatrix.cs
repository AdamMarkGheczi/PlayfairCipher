namespace Playfair
{
    internal class KeyMatrix
    {

        private char[,] values;
        public char omittedLetter;
        public char omittedLetterPair;

        private int height = 5;
        private int width = 5;

        public KeyMatrix(string input, char omittedLetter, char omittedLetterPair)
        {
            this.omittedLetter = omittedLetter;
            this.omittedLetterPair = omittedLetterPair;

            input = input.ToLower();
            values = new char[height, width];

            for (int i = 0; i < width * height; i++)
                values[i / width, i % width] = input[i];
        }

        public char this[int i, int j]
        {
            get
            {
                int rows = values.GetLength(0);
                int cols = values.GetLength(1);

                i = (i % rows + rows) % rows;
                j = (j % cols + cols) % cols;

                return values[i, j];
            }

        }

        public (int row, int col) FindLetter(char letter)
        {
            for (int i = 0; i < values.GetLength(0); i++)
                for (int j = 0; j < values.GetLength(1); j++)
                    if (values[i, j] == letter) return (i, j);

            return (-1, -1);
        }
    }
}
