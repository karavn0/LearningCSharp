using System;

namespace Lesson2
{
    internal class Program
    {
        enum Figure
        {
            Cross,
            Circle,
            Null
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Напечатаю я вам сейчас схему тюремного заключения!\n\n");
            Figure[,] boardState = new Figure[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    boardState[i, j] = Figure.Null;
                }
            }
            boardState[0, 0] = Figure.Circle;
            boardState[1, 1] = Figure.Cross;
            DrawBoard(boardState);
        }

        private static void DrawBoard(Figure[,] boardState)
        {
            Console.Clear();
            Console.WriteLine("-------------");
            for (int i = 0; i < 3; i++)
            {
                Console.Write("|");
                for (int j = 0; j < 3; j++)
                {
                    int currentCell = i * 3 + j + 1;
                    Console.Write(" ");
                    FillCell(boardState[i, j], currentCell);
                    Console.Write(" |");
                }
                Console.WriteLine("\n-------------");
            }
            return;
        }

        private static void FillCell(Figure figure, int defaultNumber)
        {
            switch (figure)
            {
                case Figure.Cross:
                    DrawSymbol('X', ConsoleColor.DarkRed);
                    break;
                case Figure.Circle:
                    DrawSymbol('O', ConsoleColor.DarkBlue);
                    break;
                default:
                    Console.Write(defaultNumber);
                    break;
            }
            return;
        }

        private static void DrawSymbol(char symbol, ConsoleColor backgroundColor)
        {
            ConsoleColor defaultColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(symbol);
            Console.BackgroundColor = defaultColor;
            return;
        }
    }
}
