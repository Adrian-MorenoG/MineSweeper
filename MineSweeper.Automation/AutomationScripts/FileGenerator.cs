using MineSweeper.Game.Models;
using System;
using System.IO;

namespace MineSweeper.Automation
{
    class FileGenerator
    {
        private static string FILENAMEALLPOSITIONS = "AllPositions.txt";

        public static void GenerateAllPositions(Board board)
        {
            try
            {
                string fileName = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Inputs", FILENAMEALLPOSITIONS);

                //Delete the file if exists.
                if (File.Exists(fileName))
                    File.Delete(fileName);

                // Create a file to write.
                using StreamWriter sw = File.CreateText(fileName);

                //Write all positions of the board.
                foreach (BoardCell boardCell in board.Cells)
                {
                    sw.WriteLine(string.Format("S {0} {1}", boardCell.Position.X, boardCell.Position.Y));
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
