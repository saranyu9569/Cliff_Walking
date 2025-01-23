using QLearningHw;

class Program
{
    static void Main(string[] args)
    {
        QLearning qLearning = new QLearning();
        qLearning.Train(100000);
        qLearning.ShowBestPath();
        qLearning.PrintQTable();
    }
}