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
            string[,] positions = new string[ROWS,COLUMNS];
            List<String> previousInputs = new List<String>();
            string position;
            bool isComputer = false;
            bool win = false;     
            bool isInputInt = false;
                
                //Fill array to populate the board positions
                FillArray(positions);
                do {
                    // Print the board
                    UserInterface.PrintBoard(positions);
                    //If isComputer is true, generate a guess for the computer else the user inputs their position
                    if(isComputer)
                    {
                        position = ComputerGuess();
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
                        if(isComputer)
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
                    win = CheckWin(positions);
                    
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

        //Private method for generating the computers guess
        private static string ComputerGuess(){
            Random rnd = new Random();
            string position = rnd.Next(RANGE_START,RANGE_END).ToString();
            return position;
        }

        //Private method for filling the array
        private static void FillArray(string [,] positions){
            int count = 1;
            for(int i = 0; i < positions.GetLength(0); i++)
            {
                for(int j = 0; j < positions.GetLength(1); j++)
                {  
                    positions[i,j] = count.ToString();
                    count++; 
                }
            }
        }
        
        //Private method for checking if there is a winner
        private static bool CheckWin(string[,] positions)
        {
            int rowWinCheck;
            int columnWinCheck;
            int diagonalWinCheck;
            bool win = false;
            
            //Check horizontal win
            if(!win) {
                for(int row = 0; row < ROWS; row++)
                {   
                    rowWinCheck = 0; 
                    for(int column = 0; column < COLUMNS; column++)
                    {  
                        if(positions[row,0] == positions[row,column])
                        {
                            rowWinCheck++;
                        }
                    }

                    if(rowWinCheck == ROWS)
                    {
                        win = true;
                    }     
                }
            }

            //Check vertical win
            if(!win)
            {
                for(int row = 0; row < ROWS; row++)
                {   
                    columnWinCheck = 0;
                    for(int column = 0; column < COLUMNS; column++)
                    {
                        if(positions[0, row] == positions[column,row])
                        {
                            columnWinCheck++;       
                        }
                    }

                    if(columnWinCheck == COLUMNS)
                    {
                        win = true;
                    }
                }
            }

            //Check diagonal win left to right
            if(!win)
            {
                diagonalWinCheck = 0;
                for(int row = 0; row < ROWS; row++)
                {
                
                    if(positions[0, 0] == positions[row,row]) 
                    {
                        diagonalWinCheck++;
                    }
                        
                    if(diagonalWinCheck == ROWS)
                    {
                        win = true;
                    }              
                }

            }

            //Check diagonal win right to left
            if(!win)
            {
                diagonalWinCheck = 0;
                for(int row = 0; row < ROWS; row++)
                {
                    if(positions[0, COLUMNS - 1] == positions[row,COLUMNS - 1 - row]) 
                    {
                        diagonalWinCheck++;
                    }
                        
                    if(diagonalWinCheck == ROWS)
                    {
                        win = true;
                    }          
                }
            }
      
            return win;
        }
    }
}