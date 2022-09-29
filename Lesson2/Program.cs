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
            Figure[,] boardState = new Figure[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    boardState[i, j] = Figure.Null;
                }
            }
            DrawBoard(boardState);
            Console.WriteLine("Добро пожаловать в могилы-воронки! Игрок #1, выберите фигуру:");
            int firstPlayerChoice = 0;
            while (true)
            {
                Console.WriteLine("1 - крестики, 2 - нолики");
                string firstPlayerInput = Console.ReadLine();
                if (
                    int.TryParse(firstPlayerInput, out firstPlayerChoice)
                    && firstPlayerChoice >= 1
                    && firstPlayerChoice <= 2
                    ) break;
            };
            bool crossesNow = firstPlayerChoice == 1;
            bool firstPlayerNow = true;
            string message = "";
            while (true)
            {
                DrawBoard(boardState);
                PromptMove(ref boardState, firstPlayerNow ? 1 : 2, crossesNow);
                if (CheckVictory(ref boardState, firstPlayerChoice == 1, out message)) break;
                crossesNow = !crossesNow;
                firstPlayerNow = !firstPlayerNow;
            }
            DrawBoard(boardState);
            Console.WriteLine(message);
            Console.WriteLine("Ещё партию? y/n");
            if (Console.ReadLine()[0] == 'y') Main(args);
        }

        private static void PromptMove(ref Figure[,] boardState, int playerNum, bool crossesNow)
        {
            Console.Write($"Игрок {playerNum}, введите № клетки: ");
            int selectedCell = 0;
            while (true)
            {
                string playerInput = Console.ReadLine();
                if (!int.TryParse(playerInput, out selectedCell))
                {
                    Console.WriteLine($"Игрок {playerNum}, вы ввели нечто невразумительное. Повторите ввод");
                    continue;
                }
                if (selectedCell < 1 || selectedCell > 9)
                {
                    Console.WriteLine("Такой клетки не существует");
                    continue;
                };
                int i = (selectedCell - 1) / 3;
                int j = (selectedCell - 1) % 3;
                if (boardState[i, j] != Figure.Null)
                {
                    Console.WriteLine("Это место уже занято");
                    continue;
                }
                boardState[i, j] = crossesNow ? Figure.Cross : Figure.Circle;
                break;
            }
        }

        private static bool AllCellsOccupied(ref Figure[,] boardState)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (boardState[i, j] == Figure.Null) return false;
                }
            }
            return true;
        }

        private static bool CheckCombinations(ref Figure[,] boardState, Figure figure)
        {
            for (int i = 0; i < 3; i++)
            {
                int cnt1, cnt2;
                cnt1 = cnt2 = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (boardState[i, j] == figure) cnt1++;
                    if (boardState[j, i] == figure) cnt2++;
                }
                if (cnt1 == 3 || cnt2 == 3) return true;
            }
            if (boardState[0, 0] == figure 
                && boardState[1, 1] == figure 
                && boardState[2, 2] == figure) return true;
            if (boardState[0, 2] == figure
                && boardState[1, 1] == figure
                && boardState[2, 0] == figure) return true;
            return false;
        }

        private static bool CheckVictory(ref Figure[,] boardState, bool firstPlayerCrosses, out string message)
        {
            bool crossesWon = CheckCombinations(ref boardState, Figure.Cross);
            bool circlesWon = CheckCombinations(ref boardState, Figure.Circle);
            if (crossesWon)
            {
                message = firstPlayerCrosses ? "Победил 1 игрок!" : "Победил 2 игрок!";
                return true;
            }
            if (circlesWon)
            {
                message = firstPlayerCrosses ? "Победил 2 игрок!" : "Победил 1 игрок!";
                return true;
            }
            if (AllCellsOccupied(ref boardState))
            {
                message = "Ничья!";
                return true;
            }
            message = "";
            return false;
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
