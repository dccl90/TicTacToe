using System;
using System.Linq;
namespace TicTacToe
{
    
    class Program
    {
        static void Main(string[] args)
        {
            string[,] positions = GameLogic.GetPositions();
            GameLogic.InitGameBoard();
            UserInterface.PlayGame(positions);
        }      
    }
}