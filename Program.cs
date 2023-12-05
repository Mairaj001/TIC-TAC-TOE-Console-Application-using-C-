// using System;



namespace TicTacToe
{
    internal class MainLoop
    {

        public static string message = "";
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            var game = new TicTacToe();
            do
            {
                game.ResetGrid();
                game.Start();
                Console.WriteLine("Do you want to play more Y for Yes N for no");
                message = Console.ReadLine();


            } while (message == "Y" || message == "y");



        }
    }

    internal class TicTacToe
    {
        private UserInterface UI;
        private char[] grid = { '-', '-', '-', '-', '-', '-', '-', '-', '-' };
        private char currentPlayer = 'X';

        public TicTacToe()
        {
            UI = new UserInterface();
        }

        public void Start()
        {
            Console.Clear();

            while (!IsGridFull())
            {


                UI.DisplayGrid(grid);
                Console.WriteLine($"\t\t\t\tPlayer {currentPlayer}'s turn. Enter a position (0-8): ");

                int position;
                while (true)
                {
                    position = int.Parse(Console.ReadLine());

                    if (IsValidMove(position))
                        break;

                    Console.WriteLine("Invalid input. Please enter a valid position (0-8): ");
                }

                grid[position] = currentPlayer;

                if (CheckWinner())
                {
                    UI.DisplayGrid(grid);
                    Console.WriteLine($"\n\n\t\t\t\tPlayer {currentPlayer} wins!");
                    return;
                }

                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
            }

            UI.DisplayGrid(grid);
            Console.WriteLine("\n\n\t\t\t\tIt's a draw!");
        }


        private bool IsGridFull()
        {
            foreach (char i in grid)
            {
                if (i == '-')
                    return false;
            }
            return true;
        }

        public void ResetGrid()
        {

            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = '-';
            }


        }

        private bool IsValidMove(int position)
        {
            return position >= 0 && position < 9 && grid[position] == '-';
        }

        private bool CheckWinner()
        {
            if (grid[0] != '-' && ((grid[0] == grid[1] && grid[1] == grid[2]) ||
               (grid[0] == grid[3] && grid[3] == grid[6]) || (grid[0] == grid[4] && grid[4] == grid[8])))
            {

                return true;
            }
            else if (grid[4] != '-' && ((grid[1] == grid[4] && grid[4] == grid[7]) ||
                (grid[3] == grid[4] && grid[4] == grid[5]) || (grid[2] == grid[4] && grid[4] == grid[6])))
            {

                return true;
            }
            else if (grid[8] != '-' && ((grid[2] == grid[5] && grid[5] == grid[8]) ||
                (grid[6] == grid[7] && grid[7] == grid[8])))
            {

                return true;
            }
            else
            {
                // None of the conditions are true
                return false;
            }



        }
    }

    internal class UserInterface
    {
        public void DisplayGrid(char[] grid)
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t----------------TIC TAC TOE----------------");
            Console.WriteLine($"\t\t\t\t|      {grid[0]}      |      {grid[1]}      |      {grid[2]}      |");
            Console.WriteLine("\t\t\t\t ------+------ ------+------  -----+-----");
            Console.WriteLine($"\t\t\t\t|      {grid[3]}      |      {grid[4]}      |      {grid[5]}      |");
            Console.WriteLine("\t\t\t\t ------+------ ------+------  -----+-----");
            Console.WriteLine($"\t\t\t\t|      {grid[6]}      |      {grid[7]}      |      {grid[8]}      |");
            Console.WriteLine("\t\t\t\t ----------------------------------------");
        }
    }
}
