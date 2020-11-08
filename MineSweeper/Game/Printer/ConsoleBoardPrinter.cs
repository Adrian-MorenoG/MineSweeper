using System;
using System.Text;
using MineSweeper.Game.Models;

namespace MineSweeper.Game.Printer
{
    /// <summary>
    /// Prints the board in the console in a human readable format.
    /// This behaviour is controlled using the cell toString method.
    /// </summary>
    public class ConsoleBoardPrinter: IBoardPrinter
    {
        public void PrintBoard(Board board)
        {
            var builder = new StringBuilder();
            var maxX = (int)board.Size.X;
            for (var y = 0; y < board.Size.Y; y++)
            {
                for (var x = 0; x < board.Size.X; x++)
                {
                    var cell = board.Cells[x + y * maxX];
                    builder.Append($"[{cell}]");
                }    
                
                builder.Append("\n");
            }
            
            Console.WriteLine(builder.ToString());
        }

        public void PrintBoardWithCoords(Board board)
        {
            throw new NotImplementedException();
        }
    }
}