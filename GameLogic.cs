using System.IO.Compression;

namespace TicTacToe
{
    public class GameLogic
    {
        
        static string[,] positions = new string[Constants.ROWS,Constants.COLUMNS];
        static bool isComputer = false;
        static Random rnd = new Random();

        /// <summary>
        /// Fetch the positions
        /// </summary>
        /// <returns>A string array of poisitions</returns>
        public static string[,] GetPositions()
        {
            return positions;
        }

        /// <summary>
        /// Checks if it is the computers turn
        /// </summary>
        /// <returns>A boolean indicating if it is the computers turn</returns>
        public static bool IsComputer()
        {
            return isComputer;
        }

        /// <summary>
        /// Sets the isComputer boolean when it's the computers turn
        /// </summary>
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

        /// <summary>
        /// Initialises the array to fill the game board.
        /// </summary>
        public static void InitGameBoard()
        {
            string[,] positions = GetPositions();
            FillArray(positions);
        } 
        
        /// <summary>
        /// Fills the positions on the board
        /// </summary>
        /// <param name="positions">A string array to store the positions</param>
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

        /// <summary>
        /// Method for generating the computers guess
        /// </summary>
        /// <returns>The position the computer selected on the board</returns>
        public static string ComputerGuess()
        {
            string position = rnd.Next(Constants.RANGE_START, Constants.RANGE_END).ToString();
            return position;
        } 

        /// <summary>
        /// Checks if the position being input is valid
        /// </summary>
        /// <param name="position">The array of positions</param>
        /// <returns>If the position input is valid</returns>
        public static bool IsInputValid(string position)
        {
            bool isInputValid = false;
            bool isInputInt = int.TryParse(position, out int input);
            //Validate the input is an int and within the acceptable range
            if(isInputInt && input >= Constants.MIN_INPUT && input <= Constants.MAX_INPUT)
            {
                isInputValid = true;
            }

            return isInputValid;
        }

        /// <summary>
        /// Checks if the position input is availble
        /// </summary>
        /// <param name="position">The array of positions on the board</param>
        /// <returns>If the position input is available</returns>
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

        /// <summary>
        /// Updates the position on the board as input by the user or computer
        /// </summary>
        /// <param name="position">The position input by the user or computer</param>
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
                            positions[i,j] = Constants.INPUT_X;
                        }
                        else
                        {
                            positions[i,j] = Constants.INPUT_O;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// A method to check if the board is full
        /// </summary>
        /// <returns>If the board is full</returns>
        public static bool IsBoardFull()
        {
            int count = 0;
            bool isBoardFull = false;
            foreach(string position in positions)
            {
                if(position == Constants.INPUT_O || position == Constants.INPUT_X)
                {
                    count++;
                }

                if(count == 9)
                {
                    isBoardFull = true;
                }
            }

            return isBoardFull;
        }

        /// <summary>
        /// Check is the game has a winner
        /// </summary>
        /// <returns>A boolean indicating if the game has been won</returns>
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

        /// <summary>
        /// A private method that checks if there are three horizontal matches
        /// </summary>
        /// <returns>A boolean indicating there were three horizontal matches</returns>
        private static bool CheckHorizontalWin()
        {
            int rowWinCheck;
            bool win = false;
            for(int row = 0; row < Constants.ROWS; row++)
            {   
                rowWinCheck = 0; 
                for(int column = 0; column < Constants.COLUMNS; column++)
                {  
                    if(positions[row,0] == positions[row,column])
                    {
                        rowWinCheck++;
                    }
                }

                if(rowWinCheck == Constants.ROWS)
                {
                    win = true;
                }     
            }
            return win;
        }

        /// <summary>
        /// A private method that checks if there are three vertical matches
        /// </summary>
        /// <returns>A boolean indicating there were three vertical matches</returns>
        private static bool CheckVerticalWin()
        {   
            int columnWinCheck;
            bool win = false;

            for(int row = 0; row < Constants.ROWS; row++)
            {   
                columnWinCheck = 0;
                for(int column = 0; column < Constants.COLUMNS; column++)
                {
                    if(positions[0, row] == positions[column,row])
                    {
                        columnWinCheck++;       
                    }
                }

                if(columnWinCheck == Constants.COLUMNS)
                {
                    win = true;
                }
            }
            return win;
        }

        /// <summary>
        /// A private method that checks if there are three diagonal matches
        /// </summary>
        /// <returns>A boolean indicating there were three diagonal matches</returns>
        private static bool CheckDiagonalWin()
        {
            int diagonalWinCheck = 0;
            bool win = false;
            for(int row = 0; row < Constants.ROWS; row++)
            {
                if(positions[0, 0] == positions[row,row]) 
                {
                    diagonalWinCheck++;
                }
                    
                if(diagonalWinCheck == Constants.ROWS)
                {
                    return true;
                }              
            }

            diagonalWinCheck = 0;
            for(int row = 0; row < Constants.ROWS; row++)
            {
                if(positions[0, Constants.COLUMNS - 1] == positions[row, Constants.COLUMNS - 1 - row]) 
                {
                    diagonalWinCheck++;
                }
                    
                if(diagonalWinCheck == Constants.ROWS)
                {
                    win = true;
                }          
            }
            return win;
        }
        
    }
}