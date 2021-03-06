﻿using System.Text;
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
            
            MineSweeperConsole.WriteLine(builder.ToString());
        }

        public void PrintBoardWithCoords(Board board)
        {
            var builder = new StringBuilder();
            var maxX = (int) board.Size.X;

            // Build X axis
            builder.Append("   ");
            for (var i = 0; i < maxX; i++)
            {
                builder.Append($" {i} ");
            }

            builder.Append("\n   ");
            for (var i = 0; i < maxX; i++)
            {
                builder.Append("---");
            }

            builder.Append("\n");
            
            // Build Y axis and map
            for (var y = 0; y < board.Size.Y; y++)
            {
                builder.Append($"{y}| ");
                for (var x = 0; x < board.Size.X; x++)
                {
                    var cell = board.Cells[x + y * maxX];
                    builder.Append($"[{cell}]");
                }    
                
                builder.Append("\n");
            }

            MineSweeperConsole.WriteLine(builder.ToString());
        }
    }
}