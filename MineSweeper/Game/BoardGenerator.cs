using System;
using System.Collections.Generic;
using System.Numerics;
using MineSweeper.Game.Models;
using MineSweeper.Game.Printer;

namespace MineSweeper.Game
{
    public interface IBoardGenerator
    {
        Board GenerateBoard(BoardOptions options);
    }

    /// <summary>
    /// Generates the board model depending on the options passed
    /// in the [GenerateBoard] method.
    /// Random mines are placed in the board depending on the seed
    /// specified in the options class. If the seed is not provided,
    /// the system time is used.
    /// </summary>
    public class BoardGenerator : IBoardGenerator
    {
        public Board GenerateBoard(BoardOptions options)
        {
            var maxX = (int) options.Size.X;
            var maxY = (int) options.Size.Y;
            
            var cellsNum = (int) (options.Size.X * options.Size.Y);
            var cells = new BoardCell[cellsNum];
            
            var posRandomGenerator = new Random(options.Seed);
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

            for (var y = 0; y < maxY; y++)
            {
                for (var x = 0; x < maxX; x++)
                {
                    var position = new Vector2(x, y);
                    var cell = new BoardCell
                    {
                        Position = position,
                        IsMine = minePositions.Contains(position),
                        NeighbouringCells = 0,
                        Status = CellStatus.HIDDEN
                    };
                     
                    cells[x + y * maxX] = cell;
                }    
            }

            var validNeighbourPositions = new[]
            {
                new Vector2(-1, -1),
                new Vector2(0, -1),
                new Vector2(1, -1),
                new Vector2(-1, 0),
                new Vector2(1, 0),
                new Vector2(-1, 1),
                new Vector2(0, 1),
                new Vector2(1, 1)
            };

            foreach (var minePosition in minePositions)
            {
                foreach (var validNeighbourPosition in validNeighbourPositions)
                {
                    var currentPosition = minePosition + validNeighbourPosition;

                    if (currentPosition.X >= 0
                        && currentPosition.Y >= 0
                        && currentPosition.X < maxX
                        && currentPosition.Y < maxY)
                    {
                        cells[(int) (currentPosition.X + currentPosition.Y * maxX)].NeighbouringCells++;
                    }
                }
            }
      
            return new Board(options.Mines, options.Size, cells);;
        }
    }
}