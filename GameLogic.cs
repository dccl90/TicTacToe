namespace TicTacToe
{
    public class GameLogic
    {
        
        
        //Private method for generating the computers guess
        public static string ComputerGuess(Random rnd, int rangeStart, int rangeEnd){
            string position = rnd.Next(rangeStart,rangeEnd).ToString();
            return position;
        }

    }
}