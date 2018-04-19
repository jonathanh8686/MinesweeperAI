using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperAI
{
    /// <summary>
    /// Class to simulate the game of Minesweeper
    /// </summary>
    class Game
    {
        int w, h, n;
        bool[,] board;
        int[,] userBoard;

        /// <summary>
        /// Initalize Game board
        /// </summary>
        /// <param name="w"># of cells in X axis</param>
        /// <param name="h"># of cells in Y axis</param>
        /// <param name="n"># of bombs on board</param>
        public Game(int w, int h, int n)
        {
            this.w = w;
            this.h = h;
            board = new bool[w, h];
            userBoard = new int[w, h];

            this.n = n;

            placeMines(n);
        }

        private void placeMines(int n)
        {
            if (n > w * h)
            {
                Console.WriteLine("Too many mines for board size!");
                Console.WriteLine("Changing to maximum...");
                placeMines(w * h);
                return;
            }

            Random r = new Random();
            for (int i = 0; i < n; i++)
            {
                int x = r.Next(0, w), y = r.Next(0, h);
                while (board[x, y]) { r.Next(0, w); y = r.Next(0, h); };
                board[x, y] = true;
            }
        }

        public void makeMove(int x, int y)
        {
            if (board[x, y])
            {
                Console.WriteLine("Game Over!!");
                resetGame();
            }
            else
            {
                userBoard[x, y] = countNeighboringBombs(x, y);
                if (userBoard[x, y] == 0)
                {
                    userBoard[x, y] = -1;
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (x + i < w && x + i >= 0 && y + j < h && y + j >= 0 && !(j == 0 && i == 0))
                                if (userBoard[x + i, y + j] != -1)
                                    makeMove(x + i, y + j);
                        }
                    }
                }

            }
        }

        private int countNeighboringBombs(int x, int y)
        {
            int res = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (x + i < w && x + i >= 0 && y + j < h && y + j >= 0)
                        if (board[x + i, y + j])
                            res++;
                }
            }
            return res;
        }

        private void resetGame()
        {
            board = new bool[w, h];
            userBoard = new int[w, h];

        }

        public void printUserBoard()
        {
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                    Console.Write(userBoard[i, j] + "\t");
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        public void printBoard()
        {
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                    Console.Write(Convert.ToInt32(board[i, j])  + "\t");
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        public void printCell(int x, int y)
        {
            Console.WriteLine($"Cell value at {x}, {y} is {board[x, y]}");
        }
    }
}
