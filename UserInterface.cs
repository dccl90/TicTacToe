namespace TicTacToe
{
    public static class UserInterface
    {
        /// <summary>
        /// Prints the game board to the screen
        /// </summary>
        /// <param name="positions"></param>
        public static void PrintBoard(string[,] positions)
        {
            ClearUserInterface();
            PrintHeader(Constants.INPUT_O, Constants.INPUT_X);
            for(int i = 0; i < positions.GetLength(0); i++)
            {
                for(int j = 0; j < positions.GetLength(1); j++)
                {  
                    Console.Write($"|__{positions[i,j]}__|");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Sets the player or computer position on the game board
        /// </summary>
        /// <param name="isComputer">Used to track if the player or computer is setting the position</param>
        /// <returns></returns>
        public static string SetPosition(bool isComputer)
        {   
            string position;
            if(isComputer)
            {
               position = GameLogic.ComputerGuess();
            }
            else
            {
                Console.Write("Enter Your Position: ");
                position = Console.ReadLine();
            }
            
            return position;
        }

        /// <summary>
        /// Prints a message indicating the position being set is invalid
        /// </summary>
        public static void PrintInvalidPositionMessage()
        {
            Console.WriteLine($"Enter a valid number between {Constants.MIN_INPUT} and {Constants.MAX_INPUT}");
            Console.WriteLine("Press Any Key to Continue");
            Console.ReadLine();
        }

        /// <summary>
        /// Prints a message indicating the the position has already been filled
        /// </summary>
        public static void PrintPositionFilledMessage()
        {
            Console.WriteLine("Position has already been filled");
            Console.WriteLine("Press Any Key to Continue");
            Console.ReadLine();
        }

        /// <summary>
        /// Prints a message indicating the board is full
        /// </summary>
        public static void PrintBoardIsFullMessage()
        {
            Console.WriteLine("No Winner! The Board is Full");
        }

        /// <summary>
        /// Prints the winner a message indicating the winner of the game
        /// </summary>
        /// <param name="isComputer">Track if the computer or player won</param>
        /// <param name="win">Tracks if the game has been won</param>
        public static void PrintWinner(bool isComputer, bool win)
        {
            if(win)
            {
                if(isComputer)
                {
                    Console.WriteLine("Computer wins");
                }
                else 
                {
                    Console.WriteLine("Player One Wins");
                }
            }   
        }

        /// <summary>
        /// Used to print the header indicating the player and computer
        /// </summary>
        /// <param name="playerOne">Indicates player one</param>
        /// <param name="computer">Indicates the computer</param>
        private static void PrintHeader(string playerOne, string computer)
        {
            Console.WriteLine($"Player1:{playerOne} and Computer:{computer}");
            Console.WriteLine();
        }

        /// <summary>
        /// Clears the user interface
        /// </summary>
        private static void ClearUserInterface()
        {
            Console.Clear();
        }

        /// <summary>
        /// Starts the game loop
        /// </summary>
        /// <param name="positions">Array of strings with the boards positions</param>
        public static void PlayGame(string[,] positions)
        {
            string position;
            bool win = false;    
            
            do{
                PrintBoard(positions);
                bool isComputer = GameLogic.IsComputer();
                //If isComputer is true, generate a guess for the computer else the user inputs their position
                //position = GameLogic.ComputerGuess();
                position = SetPosition(isComputer);
                
                //Check if user input is valid
                bool isInputValid = GameLogic.IsInputValid(position);
                //If input is invalid then print the invalid message 
                if(!isInputValid)
                {
                    PrintInvalidPositionMessage();
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
                        PrintPositionFilledMessage();
                    }  
                    continue;    
                }

                //Update the position on the board
                GameLogic.UpdatePosition(position);

                //Print the board with the updated positions
                PrintBoard(positions);

                //Check if there is a winner
                win = GameLogic.CheckWin();
                //If there is a winner print the winner message
                PrintWinner(isComputer, win);

                
                // Check if board is Full
                bool isBoardFull = GameLogic.IsBoardFull();
                //If board is full and there is no winner print Board is Full message
                if(isBoardFull && !win)
                {
                    PrintBoardIsFullMessage();
                    break;
                }
                
                //Set computers turn
                GameLogic.SetIsComputer();
            }
            while(!win);
        }
    }
}