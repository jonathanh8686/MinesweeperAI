using System;

namespace MinesweeperAI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "MinesweeperAI";
            Game g1 = new Game(8, 8, 10);

            g1.printBoard();
            g1.makeMove(0, 0);
            g1.printUserBoard();

            Console.ReadKey();
        }
    }
}
