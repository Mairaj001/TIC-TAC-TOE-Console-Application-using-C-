using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTask1
{


    internal class TicTacToe
    {
        private UserInterface UI;

        public TicTacToe()
        {
            UI = new UserInterface();
        }
        public bool IsCellEmpty(int x, int y)
        { return UI.grid[x, y] == '\0'; }

        public void PlayerTurnUpdate()
        {
            if (UI.currentTurn == UserInterface.PLAYER.Player01) { UI.currentTurn = UserInterface.PLAYER.Player02; }
            else { UI.currentTurn = UserInterface.PLAYER.Player01; }
        }

        public bool IsGridFull()
        {
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (UI.grid[i, j] == '\0')
                    { return false; }
                }
            }
            return true;
        }

        public void SetCellValueOfGrid(int x, int y, UserInterface.PLAYER pCurrentTurn)
        {
            if (pCurrentTurn == UserInterface.PLAYER.Player01) { UI.grid[x, y] = 'X'; }
            else { UI.grid[x, y] = 'O'; }
        }
        public UserInterface.PLAYER CheckWinner()
        {
            if (
                 (UI.grid[0, 0] == 'X' && UI.grid[0, 1] == 'X' && UI.grid[0, 2] == 'X') ||
                 (UI.grid[1, 0] == 'X' && UI.grid[1, 1] == 'X' && UI.grid[1, 2] == 'X') ||
                 (UI.grid[2, 0] == 'X' && UI.grid[2, 1] == 'X' && UI.grid[2, 2] == 'X') ||

                 (UI.grid[0, 0] == 'X' && UI.grid[0, 1] == 'X' && UI.grid[2, 0] == 'X') ||
                 (UI.grid[0, 1] == 'X' && UI.grid[1, 1] == 'X' && UI.grid[2, 1] == 'X') ||
                 (UI.grid[0, 2] == 'X' && UI.grid[1, 2] == 'X' && UI.grid[2, 2] == 'X') ||

                 (UI.grid[0, 0] == 'X' && UI.grid[1, 1] == 'X' && UI.grid[2, 2] == 'X') ||
                 (UI.grid[0, 2] == 'X' && UI.grid[1, 1] == 'X' && UI.grid[2, 0] == 'X')
                )
            { return UserInterface.PLAYER.Player01; }
            else if (
                   (UI.grid[0, 0] == 'O' && UI.grid[0, 1] == 'O' && UI.grid[0, 2] == 'O') ||
                   (UI.grid[1, 0] == 'O' && UI.grid[1, 1] == 'O' && UI.grid[1, 2] == 'O') ||
                   (UI.grid[2, 0] == 'O' && UI.grid[2, 1] == 'O' && UI.grid[2, 2] == 'O') ||

                   (UI.grid[0, 0] == 'O' && UI.grid[1, 0] == 'O' && UI.grid[2, 0] == 'O') ||
                   (UI.grid[0, 1] == 'O' && UI.grid[1, 1] == 'O' && UI.grid[2, 1] == 'O') ||
                   (UI.grid[0, 2] == 'O' && UI.grid[1, 2] == 'O' && UI.grid[2, 2] == 'O') ||

                   (UI.grid[0, 0] == 'O' && UI.grid[1, 1] == 'O' && UI.grid[2, 2] == 'O') ||
                   (UI.grid[0, 2] == 'O' && UI.grid[1, 1] == 'O' && UI.grid[2, 0] == 'O')
                   )
            { return UserInterface.PLAYER.Player02; }
            else { return UserInterface.PLAYER.NUN; }
        }
        public void StartGameLoop()
        {
            UI.ClearGrid();
            while (!IsGridFull())
            {
                UI.UpdateUIForTurn();
                if (IsCellEmpty(UI.rowIndex, UI.columnIndex))
                {
                    SetCellValueOfGrid(UI.rowIndex, UI.columnIndex, UI.currentTurn);

                    UserInterface.PLAYER winningPlayer = CheckWinner();
                    if (winningPlayer != UserInterface.PLAYER.NUN)
                    {
                        UI.message = "Wins" + winningPlayer;
                        UI.UpdateUIForEnd(winningPlayer);
                        break;
                    }
                }
                else { UI.message = "Cell is already filled"; }

                PlayerTurnUpdate();
            }
        }
    }

    internal class UserInterface
    {

        public enum PLAYER
        { Player01, Player02, NUN }

        public int rowIndex, columnIndex;
        public char[,] grid;
        public PLAYER currentTurn;


        public UserInterface()
        {
            currentTurn = PLAYER.Player01;
            rowIndex = 0;
            columnIndex = 0;
            grid = new char[3, 3];
        }

        public void ClearGrid()
        {
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    grid[i, j] = '\0';
                }
            }
        }

        public void PlayerInputRC()
        {
            Console.WriteLine("Enter Row and Column #");
            rowIndex = Convert.ToInt32(Console.ReadLine());
            columnIndex = Convert.ToInt32(Console.ReadLine());
            if(rowIndex >= 3 || columnIndex >= 3)
            {
                PlayerInputRC();
            }
        }

        public void displayGrid()
        {
            Console.WriteLine("\t0\t1\t2");
            Console.WriteLine("\n");
            for (int i = 0; i < 3; ++i)
            {
                Console.Write("" + i);
                for (int j = 0; j < 3; ++j)
                { Console.Write("\t" + grid[i, j]); }
                Console.WriteLine("\n");
            }
        }

        public string message;
        public void UpdateUIForTurn()
        {
            Console.Clear();
            Console.WriteLine("--------TICTACTOE GAME------------");
            displayGrid();
            if (message != null)
            { Console.WriteLine(message); }
            Console.WriteLine("Turn::" + currentTurn);
            PlayerInputRC();
        }

        public void UpdateUIForEnd(PLAYER parameterWinner)
        {
            Console.Clear();
            Console.WriteLine("--------TICTACTOE GAME------------");
            displayGrid();
            Console.WriteLine("The winner is:" + parameterWinner);
        }

    }
    internal class MainLoop
    {
        public static string continueLoop = "";
        public static TicTacToe tT;
        public static void Main(string[] args)
        {
            tT = new TicTacToe();
            do
            {
                tT.StartGameLoop();
                Console.WriteLine("Do you wish to play again? (y for yes): ");
                continueLoop = Console.ReadLine();
            } while (continueLoop == "y" || continueLoop == "Y");
        }
    }

}
