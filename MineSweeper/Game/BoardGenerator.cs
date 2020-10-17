using System;
using System.Collections.Generic;
using System.Numerics;
using MineSweeper.Game.Models;

namespace MineSweeper.Game
{
    public interface IBoardGenerator
    {
        Board GenerateBoard(BoardOptions options);
    }

    public class BoardGenerator : IBoardGenerator
    {
        public Board GenerateBoard(BoardOptions options)
        {
            var posRandomGenerator = new Random(options.Seed);
            var maxX = (int) options.Size.X;
            var maxY = (int) options.Size.Y;
            var minePositions = new List<Vector2>();
            while (minePositions.Count < options.Mines)
            {
                var x = posRandomGenerator.Next(0, maxX);
                var y = posRandomGenerator.Next(0, maxY);
                var newPos = new Vector2(x, y);

                if (!minePositions.Contains(newPos))
                {
                    minePositions.Add(newPos);
                }
            }

            var cellsNum = (int) (options.Size.X * options.Size.Y);
            Console.WriteLine(minePositions);
            return new Board(options.Mines, options.Size, new BoardCell[cellsNum]);
        }
    }
}