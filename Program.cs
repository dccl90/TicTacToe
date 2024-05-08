using System;
using System.Linq;
namespace TicTacToe
{
    
    class Program
    {
        static void Main(string[] args)
        {
            
            string[,] positions = GameLogic.GetPositions();
            string playerOne = GameLogic.GetPlayerOne();
            string computer = GameLogic.GetComputer();
            int minPosition = GameLogic.GetMinPosition();
            int maxPosition = GameLogic.GetMaxPosition();
            string position;
            bool win = false; 
            bool isBoardFull = false;    
                
            //Fill array to populate the board positions
            GameLogic.FillArray(positions);
            do {
                // Print the board
                bool isComputer = GameLogic.IsComputer();
                UserInterface.PrintBoard(positions, playerOne, computer);
                //If isComputer is true, generate a guess for the computer else the user inputs their position
                if(isComputer)
                {
                    position = GameLogic.ComputerGuess();
                }
                else
                {
                    position = UserInterface.SetPosition();    
                }
                
                //Check if user input is valid
                bool isInputValid = GameLogic.IsInputValid(position);
                //If input is invalid then print the invalid message 
                if(!isInputValid)
                {
                    UserInterface.InvalidPositionMessage(minPosition, maxPosition);
                    continue;
                }
                
                //Check is board position is available
                bool isPositionAvailable = GameLogic.CheckPosition(position);
                // If the position is not available print the Position filled message and continue loop
                if(!isPositionAvailable)
                {
                    //If isComputer equals false print a message stating the position is already filled 
                    if(!isComputer)
                    {
                        UserInterface.PositionFilledMessage();
                    }      
                    continue;
                }

                //Update the position on the board
                GameLogic.UpdatePosition(position);

                //Print the board with the updated positions
                UserInterface.PrintBoard(positions, playerOne, computer);

                //Check if there is a winner
                win = GameLogic.CheckWin();
                //If there is a winner print the winner message
                if(win)
                {
                    UserInterface.PrintWinner(isComputer);
                }
                
                // Check if board is Full
                isBoardFull = GameLogic.IsBoardFull();
                //If board is full and there is no winner print Board is Full message
                if(isBoardFull && !win)
                {
                    UserInterface.BoardIsFullMessage();
                    break;
                }
                
                //Set computers turn
                GameLogic.SetIsComputer();

            } while(!win);
        }      
    }
}