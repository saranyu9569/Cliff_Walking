using System;

namespace QLearningHw
{
    class QLearning
    {
        private Environment env = new Environment();
        private int rows = 6, columns = 6, totalStates, totalActions = 4;
        private double[,] QTable;
        private double alpha = 0.1, gamma = 0.6, epsilon = 0.1;
        private Random random = new Random(); // Added for consistent random generation

        public QLearning()
        {
            totalStates = rows * columns;
            InitializeQTable();
        }

        private void InitializeQTable() => QTable = new double[totalStates, totalActions];

        private int GetState(int row, int col) => row * columns + col;

        private (int, int) GetPosition(int state) => (state / columns, state % columns);

        private (int, int) TakeAction(int row, int col, int action)
        {
            // First apply the original action
            var (newRow, newCol) = action switch
            {
                0 => (Math.Max(0, row - 1), col),
                1 => (row, Math.Min(columns - 1, col + 1)),
                2 => (Math.Min(rows - 1, row + 1), col),
                3 => (row, Math.Max(0, col - 1)),
                _ => (row, col)
            };

            // Apply wind effect (20% chance to move right)
            if (random.NextDouble() < 0.2)
            {
                newCol = Math.Min(columns - 1, newCol + 1);
            }

            return (newRow, newCol);
        }

        private int GetReward(int row, int col) =>
            env.GetEnvi()[row, col] switch
            {
                'C' => -100,
                'G' => 100,
                _ => -1
            };

        private int SelectAction(int state) =>
            random.NextDouble() < epsilon ? random.Next(totalActions) : GetBestAction(state);

        private int GetBestAction(int state)
        {
            int bestAction = 0;
            for (int i = 1; i < totalActions; i++)
                if (QTable[state, i] > QTable[state, bestAction])
                    bestAction = i;
            return bestAction;
        }

        private void UpdateQValue(int currentState, int action, int reward, int nextState)
        {
            double maxNextQ = QTable[nextState, 0];
            for (int i = 1; i < totalActions; i++)
                if (QTable[nextState, i] > maxNextQ) maxNextQ = QTable[nextState, i];

            QTable[currentState, action] += alpha * (reward + gamma * maxNextQ - QTable[currentState, action]);
        }

        public void Train(int episodes = 100)
        {
            for (int episode = 0; episode < episodes; episode++)
            {
                int currentState = GetState(0, 5);
                while (true)
                {
                    (int currentRow, int currentCol) = GetPosition(currentState);

                    if (env.GetEnvi()[currentRow, currentCol] == 'G') break;

                    int action = SelectAction(currentState);
                    (int nextRow, int nextCol) = TakeAction(currentRow, currentCol, action);
                    int nextState = GetState(nextRow, nextCol);
                    int reward = GetReward(nextRow, nextCol);

                    UpdateQValue(currentState, action, reward, nextState);

                    currentState = env.GetEnvi()[nextRow, nextCol] == 'C' ? GetState(0, 5) : nextState;
                }
            }
        }

        public void ShowBestPath()
        {
            char[,] bestPath = (char[,])env.GetEnvi().Clone();
            int currentState = GetState(0, 5);

            // Mark starting position
            (int startRow, int startCol) = GetPosition(currentState);
            bestPath[startRow, startCol] = 'W';

            while (true)
            {
                (int currentRow, int currentCol) = GetPosition(currentState);
                if (env.GetEnvi()[currentRow, currentCol] == 'G')
                {
                    bestPath[currentRow, currentCol] = 'W';  // Mark goal position
                    break;
                }

                int bestAction = GetBestAction(currentState);
                (int nextRow, int nextCol) = TakeAction(currentRow, currentCol, bestAction);

                bestPath[currentRow, currentCol] = 'W';

                currentState = GetState(nextRow, nextCol);
            }

            Console.WriteLine("Best path:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(bestPath[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public void PrintQTable()
        {
            Console.WriteLine();
            Console.WriteLine("Q-Table:");
            Console.WriteLine(new string('-', 90));
            Console.Write("State\t");
            for (int action = 0; action < totalActions; action++)
            {
                switch (action)
                {
                    case 0:
                        Console.Write($"  Up\t\t");
                        break;
                    case 1:
                        Console.Write($"Right\t\t");
                        break;
                    case 2:
                        Console.Write($" Down\t\t");
                        break;
                    case 3:
                        Console.Write($" Left\t\t");
                        break;
                }
            }
            Console.WriteLine();
            Console.WriteLine(new string('-', 90));

            for (int state = 0; state < totalStates; state++)
            {
                Console.Write($"{state}\t");
                for (int action = 0; action < totalActions; action++)
                {
                    Console.Write($"{QTable[state, action]:F2}\t\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine(new string('-', 90));
        }
    }
}