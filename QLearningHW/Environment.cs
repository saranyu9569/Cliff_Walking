using System;

namespace QLearningHw
{
    class Environment
    {
        public char[,] envi;
        private int rows = 6;
        private int columns = 6;

        public Environment()
        {
            envi = new char[rows, columns];
            CreateEnvironment();
        }

        public void CreateEnvironment()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (j == 5 && (i != 0 && i != 5))
                    {
                        envi[i, j] = 'C';
                    }
                    else if (i == 5 && j == 5)
                    {
                        envi[i, j] = 'G';
                    }
                    else
                    {
                        envi[i, j] = '*';
                    }
                }
            }
            envi[0, 5] = 'S';
        }

        public char[,] GetEnvi()
        {
            return envi;
        }

        public void PrintEnvi()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(envi[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}