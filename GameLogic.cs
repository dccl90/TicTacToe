namespace TicTacToe
{
    public class GameLogic
    {
        const int ROWS = 3;
        const int COLUMNS = 3;

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
        public static bool CheckWin(string[,] positions)
        {
            bool win = false;

            //Check horizontal win
            if(!win) {
                win = CheckHorizontalWin(positions);
            }

            //Check vertical win
            if(!win)
            {
                win = CheckVerticalWin(positions);
            }

            //Check diagonal win left to right
            if(!win)
            {
                win = CheckDiagonalWin(positions);
            }
            
            return win;
        }
        
        //Private method for generating the computers guess
        public static string ComputerGuess(Random rnd, int rangeStart, int rangeEnd){
            string position = rnd.Next(rangeStart,rangeEnd).ToString();
            return position;
        }

        private static bool CheckHorizontalWin(string[,] positions)
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

        private static bool CheckVerticalWin(string[,] positions)
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

        private static bool CheckDiagonalWin(string[,] positions)
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