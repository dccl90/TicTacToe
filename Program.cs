using System;
using System.Linq;
namespace TicTacToe
{
    
    class Program
    {
        const int ROWS = 3;
        const int RANGE_START = 1;
        const int RANGE_END = 10;
        const int COLUMNS = 3;
        const int MAX_INPUT = 9;
        const int MIN_INPUT = 1;
        const string INPUT_O = "O";
        const string INPUT_X = "X";
        
        static void Main(string[] args)
        {
            Random rnd = new Random();
            string[,] positions = new string[ROWS,COLUMNS];
            List<String> previousInputs = new List<String>();
            string position;
            bool isComputer = false;
            bool win = false;     
            bool isInputInt = false;
                
            //Fill array to populate the board positions
            GameLogic.FillArray(positions);
            do {
                // Print the board
                UserInterface.PrintBoard(positions);
                //If isComputer is true, generate a guess for the computer else the user inputs their position
                if(isComputer)
                {
                    position = GameLogic.ComputerGuess(rnd, RANGE_START, RANGE_END);
                }
                else
                {
                    position = UserInterface.SetPosition();    
                }
                
                //Validate the input can be parsed to an int
                isInputInt = int.TryParse(position, out int input);

                //Validate the input is an int and within the acceptable range
                if(!isInputInt || input < MIN_INPUT || input > MAX_INPUT)
                {
                    UserInterface.InvalidPositionMessage();
                    continue;
                }
                
                // If the position exists in the previous inputs continue
                if(previousInputs.Exists(x => x.Equals(position)))
                {
                    //If isComputer equals false print a message stating the position is already filled 
                    if(!isComputer)
                    {
                        UserInterface.PositionFilledMessage();
                    }      
                    continue;
                }

                //Store the position in the previousInputs list
                previousInputs.Add(position);

                //Loop over the positions array and update the value with an X or O
                for(int i = 0; i < positions.GetLength(0); i++)
                {
                    for(int j = 0; j < positions.GetLength(1); j++)
                    {  
                        if(position == positions[i,j])
                        {
                            if(isComputer){
                                positions[i,j] = INPUT_X;
                            }
                            else
                            {
                                positions[i,j] = INPUT_O;
                            }
                        }
                    }
                }

                //Print the board with the updated positions
                UserInterface.PrintBoard(positions);
                //Check if there is a winner
                win = GameLogic.CheckWin(positions);
                //If there is a winner print the winner message
                if(win)
                {
                    UserInterface.PrintWinner(isComputer);
                }
                
                //Switch between the computer and player one
                if(isComputer) 
                {
                    isComputer = false;
                } 
                else 
                {
                    isComputer = true;
                }


            } while(!win);
        }      
    }
}