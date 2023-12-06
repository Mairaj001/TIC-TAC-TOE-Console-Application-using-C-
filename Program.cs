using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTask1   
{
    internal class MainLoop //Created the internal classs Named as MainLoop beacuse it is used to restrict the access of a class or its member to within the same assembly
    {
        public static string continueLoop = "";  
        public static TicTacToe tT;  // created the object of the TICTACTOE class 
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;  

            Console.BackgroundColor = ConsoleColor.Blue;
            tT = new TicTacToe();
            // it is the loop of the board when runs untill user want to quit the game
            do
            {

                tT.StartGameLoop();
                Console.WriteLine("Do you wish to play again? (y for yes): ");
                continueLoop = Console.ReadLine();
            } while (continueLoop == "y" || continueLoop == "Y");
        }
    }

    internal class TicTacToe   // crated the internal class TicTacToe
    {
        private UserInterface UI;  // created the object of the User interface but this obejct is null yet

        public TicTacToe()   // constructor of the TICTACTOE in this it intialize all the methods and datatypes of the UI class to the UI object
        {
            UI = new UserInterface();  
        }
        public bool IsCellEmpty(int x, int y)  // This function is used to check that eact cell of the grid is empty or not if it is empty then return true otherwise will retrun the false
        { return UI.grid[x, y] == '\0'; }

        public void PlayerTurnUpdate()   // This function is used to upadate the Turn of the player and assign the PLAYER => player1 or Player2
        {
            UI.currentTurn = UI.currentTurn == UserInterface.PLAYER.Player01 ? UserInterface.PLAYER.Player02 : UserInterface.PLAYER.Player01;

        }

        public bool IsGridFull()  //It is the boolean function which checks that your game board grids contain X or O if it conatains the symbols of the player then it will retrun the true otherwise retrun false
        {
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (UI.grid[i, j] == '\0') // where '\0' represents the null character or the null terminator 
                    { return false; }
                }
            }
           
            return true;
        }

        public void SetCellValueOfGrid(int x, int y, UserInterface.PLAYER pCurrentTurn) // This function sets the value to the grid with respect to the player if Player A is playing at a moment then it will place X to the grid where "int X" and "int Y" shows the grid indexs where to palce the value
        {
            UI.grid[x, y] = pCurrentTurn == UserInterface.PLAYER.Player01 ? 'X' : 'O';

        }
        public UserInterface.PLAYER CheckWinner()  // This function is the major function of the game it checks that who will win the game according to the rules of the game 1) IF there is  horizontally or vertically same elements in the grid the player wins otherwise not 
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
            { return UserInterface.PLAYER.Player01; }  // this checks for the Player1
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
            { return UserInterface.PLAYER.Player02; } // this checks for the player 2
            else { return UserInterface.PLAYER.NUN; }
        }
        public void StartGameLoop()   // this function runs the whole game 
        {
            UI.ClearGrid(); // it will clear the grid if one game is finished
            while (!IsGridFull())   // Isgridfull is function whick checks that weather game board is Full of playes moves or not it will run untill board get full
            {
                UI.UpdateUIForTurn(); // this function changes the turn of the plaeyer if playerA plays its first move then it will update the player2
                if (IsCellEmpty(UI.rowIndex, UI.columnIndex))  //this IF statemnt checks that the desired cell is empty or not if empty then genrated the messeage otherwise continue the game
                {
                    SetCellValueOfGrid(UI.rowIndex, UI.columnIndex, UI.currentTurn); // this function set the value to the cell If playerA plays its first move on grid [0,0] it will place  X to the grid [0,0]

                    UserInterface.PLAYER winningPlayer = CheckWinner(); // here there is an instance created of the enum PLAYER  which is decleard in the UI class where checkwinner return the plaeyer which wins the game
                    if (winningPlayer != UserInterface.PLAYER.NUN) // this if checks that if there is player1 or playter2 in the winng player object
                    {
                        UI.message = "Wins" + winningPlayer;
                        UI.UpdateUIForEnd(winningPlayer);
                        break;
                    }
                   else if(winningPlayer == UserInterface.PLAYER.NUN&&IsGridFull()) // this checks that if winnng player is => PLAYER.NUN and grid is full that means that there is no winner and game draws
                    {
                        Console.WriteLine("Game draws");
                        
                        break;
                    }
               
                }
                else { UI.message = "Cell is already filled"; }

                PlayerTurnUpdate(); // at last it will update the turn of the player if upper if dosnot meet the conditons
            }
        }
    }

    internal class UserInterface  // this is the userInterface class mainly deals with the player and the game board
    {

        public enum PLAYER
        { Player01, Player02, NUN }

        public int rowIndex, columnIndex;
        public char[,] grid;
        public PLAYER currentTurn;


        public UserInterface()  // constructor of the UserInterface class
        {
            currentTurn = PLAYER.Player01;
            rowIndex = 0;
            columnIndex = 0;
            grid = new char[3, 3];
        }

        public void ClearGrid()  // this funciton clear the grid making the the values null in the grid array
        {
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    grid[i, j] = '\0';
                }
            }
        }

        public void PlayerInputRC()  // this function deals with the input of the players
        {
            try
            {
                Console.WriteLine("Enter Row and Column #");
                rowIndex = Convert.ToInt32(Console.ReadLine());
                columnIndex = Convert.ToInt32(Console.ReadLine());
                if (rowIndex >= 3 || columnIndex >= 3)
                {
                    Console.WriteLine("Invalid move");
                    

                    PlayerInputRC();
                }
               
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
        }

        public void displayGrid()  // this displays the gameBoard 
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
        public void UpdateUIForTurn()  // It will update the board if first player played its move
        {
            Console.Clear();
            Console.WriteLine("--------TICTACTOE GAME------------");
            displayGrid();
            if (message != null)
            { Console.WriteLine(message); }
            Console.WriteLine("Turn::" + currentTurn);
            PlayerInputRC();
        }

        public void UpdateUIForEnd(PLAYER parameterWinner)  // It will show that if any player wins the game
        {
            Console.Clear();
            Console.WriteLine("--------TICTACTOE GAME------------");
            displayGrid();
            Console.WriteLine("The winner is:" + parameterWinner);
        }

    }
    

}
