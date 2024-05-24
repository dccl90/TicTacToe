namespace TicTacToe
{
    public static class UserInterface
    {
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

        public static void InvalidPositionMessage()
        {
            Console.WriteLine($"Enter a valid number between {Constants.MIN_INPUT} and {Constants.MAX_INPUT}");
            Console.WriteLine("Press Any Key to Continue");
            Console.ReadLine();
        }
        public static void PositionFilledMessage()
        {
            Console.WriteLine("Position has already been filled");
            Console.WriteLine("Press Any Key to Continue");
            Console.ReadLine();
        }

        public static void BoardIsFullMessage()
        {
            Console.WriteLine("No Winner! The Board is Full");
        }

        public static void PrintWinner(bool isComputer)
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

        private static void PrintHeader(string playerOne, string computer)
        {
            Console.WriteLine($"Player1:{playerOne} and Computer:{computer}");
            Console.WriteLine();
        }

        private static void ClearUserInterface()
        {
            Console.Clear();
        }

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
                    InvalidPositionMessage();
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
                        PositionFilledMessage();
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
                if(win)
                {
                    PrintWinner(isComputer);
                }
                
                // Check if board is Full
                bool isBoardFull = GameLogic.IsBoardFull();
                //If board is full and there is no winner print Board is Full message
                if(isBoardFull && !win)
                {
                    BoardIsFullMessage();
                    break;
                }
                
                //Set computers turn
                GameLogic.SetIsComputer();
            }
            while(!win);
        }
    }
}