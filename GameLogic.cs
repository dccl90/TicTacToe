using System.IO.Compression;

namespace TicTacToe
{
    public class GameLogic
    {
        const int ROWS = 3;
        const int COLUMNS = 3;
        const int MAX_INPUT = 9;
        const int MIN_INPUT = 1;
        const string INPUT_O = "O";
        const string INPUT_X = "X";
        const int RANGE_START = 1;
        const int RANGE_END = 10;
        static string[,] positions = new string[ROWS,COLUMNS];
        static bool isComputer = false;
        static Random rnd = new Random();

        
        public static string[,] GetPositions()
        {
            return positions;
        }

        public static string GetPlayerOne()
        {
            return INPUT_O;
        }

        public static string GetComputer()
        {
            return INPUT_X;
        }

        public static bool IsComputer()
        {
            return isComputer;
        }

        public static void SetIsComputer()
        {
            if(isComputer) 
            {
                isComputer = false;
            } 
            else 
            {
                isComputer = true;
            }
        }
        public static void FillArray(string [,] positions){
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

        //Method for generating the computers guess
        public static string ComputerGuess(){
            string position = rnd.Next(RANGE_START, RANGE_END).ToString();
            return position;
        }  

        //Method for validating user input
        public static bool IsInputValid(string position)
        {
            bool isInputValid = false;
            bool isInputInt = int.TryParse(position, out int input);
            //Validate the input is an int and within the acceptable range
            if(isInputInt || input >= MIN_INPUT || input <= MAX_INPUT)
            {
                isInputValid = true;
            }

            return isInputValid;
        }

        //Method for checking the if the position on the board is available
        public static bool CheckPosition(string position)
        {
            bool isPositionAvailable = false;
            foreach(string pos in positions)
            {
                if(pos == position)
                {
                    isPositionAvailable = true;
                    break;
                }
            }    
            return isPositionAvailable;
        }   

        //Method for updating the position
        public static void UpdatePosition(string position)
        {
            for(int i = 0; i < positions.GetLength(0); i++)
            {
                for(int j = 0; j < positions.GetLength(1); j++)
                {  
                    if(position == positions[i,j])
                    {
                        if(isComputer)
                        {
                            positions[i,j] = INPUT_X;
                        }
                        else
                        {
                            positions[i,j] = INPUT_O;
                        }
                    }
                }
            }
        }

        //Method for checking if there is a winner
        public static bool CheckWin()
        {
            bool win = false;

            //Check horizontal win
            if(!win) {
                win = CheckHorizontalWin();
            }

            //Check vertical win
            if(!win)
            {
                win = CheckVerticalWin();
            }

            //Check diagonal win left to right
            if(!win)
            {
                win = CheckDiagonalWin();
            }
            
            return win;
        }

        //Private methods for checkinf if there is a winner in any direction
        private static bool CheckHorizontalWin()
        {
            int rowWinCheck;
            bool win = false;
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
            return win;
        }

        private static bool CheckVerticalWin()
        {   
            int columnWinCheck;
            bool win = false;

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
            return win;
        }

        private static bool CheckDiagonalWin()
        {
            int diagonalWinCheck = 0;
            bool win = false;
            for(int row = 0; row < ROWS; row++)
            {
                if(positions[0, 0] == positions[row,row]) 
                {
                    diagonalWinCheck++;
                }
                    
                if(diagonalWinCheck == ROWS)
                {
                    win = true;
                    return win;
                }              
            }

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
            return win;
        }
    }
}