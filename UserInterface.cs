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

        public static string SetPosition()
        {   
            string position;
            Console.Write("Enter Your Position: ");
            position = Console.ReadLine();
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
    }
}