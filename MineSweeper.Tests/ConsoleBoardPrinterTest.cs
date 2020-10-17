using System;
using System.IO;
using System.Numerics;
using MineSweeper.Game.Models;
using MineSweeper.Game.Printer;
using NUnit.Framework;

namespace MineSweeper.Tests
{
    [TestFixture]
    public class ConsoleBoardPrinterTest
    {
        /// <summary>
        /// Tests that a map with all the cells hidden is printed
        /// correctly.
        /// </summary>
        [Test]
        public void TestHiddenMap()
        {
            const string map = "[█][█][█]\n" + 
                               "[█][█][█]\n" + 
                               "[█][█][█]\n\r\n";

            var cells = new[]
            {
                new BoardCell { Position = new Vector2(0, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(0, 1), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 1), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 1), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(0, 2), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 2), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 2), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
            };
            
            var board = new Board(1, new Vector2(3, 3), cells);
            using var sw = new StringWriter();
            Console.SetOut(sw);

            
            var boardPrinter = new ConsoleBoardPrinter();
            boardPrinter.PrintBoard(board);
            
            Assert.AreEqual(map, sw.ToString());
        }

        /// <summary>
        /// Test that a map with different cell status is printed correctly.
        /// The only cell that contains a mine is the [0, 3] and all the other
        /// cells are unhidden.
        /// </summary>
        [Test]
        public void TestCustomMap()
        {
            const string map = "[░][░][░]\n" +
                               "[1][1][░]\n" +
                               "[█][1][░]\n\r\n";
                          
            
            var cells = new[]
            {
                new BoardCell { Position = new Vector2(0, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(1, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(2, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(0, 2), IsMine = false, NeighbouringCells = 1, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(1, 2), IsMine = false, NeighbouringCells = 1, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(2, 2), IsMine = false, NeighbouringCells = 0, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(0, 3), IsMine = true, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 3), IsMine = false, NeighbouringCells = 1, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(2, 3), IsMine = false, NeighbouringCells = 0, Status = CellStatus.VISIBLE},
            };
            
            var board = new Board(1, new Vector2(3, 3), cells);
            using var sw = new StringWriter();
            Console.SetOut(sw);

            
            var boardPrinter = new ConsoleBoardPrinter();
            boardPrinter.PrintBoard(board);
            
            Assert.AreEqual(map, sw.ToString());
        }
        
        /// <summary>
        /// Test that a map with different cell status is printed correctly.
        /// The only cell that contains a mine is the [0, 3] and has exploded.
        /// All the other cells are hidden.
        /// </summary>
        [Test]
        public void TestCustomMap_2()
        {
            const string map = "[█][█][█]\n" +
                               "[█][█][█]\n" +
                               "[*][█][█]\n\r\n";
                          
            
            var cells = new[]
            {
                new BoardCell { Position = new Vector2(0, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(0, 2), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 2), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 2), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(0, 3), IsMine = true, NeighbouringCells = 0, Status = CellStatus.EXPLODED},
                new BoardCell { Position = new Vector2(1, 3), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 3), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
            };
            
            var board = new Board(1, new Vector2(3, 3), cells);
            using var sw = new StringWriter();
            Console.SetOut(sw);

            
            var boardPrinter = new ConsoleBoardPrinter();
            boardPrinter.PrintBoard(board);
            
            Assert.AreEqual(map, sw.ToString());
        }
         
        /// <summary>
        /// Test that a map with different cell status is printed correctly.
        /// All the cells are hidden except the [0, 0] and [2, 2] that are flagged.
        /// </summary>
        [Test]
        public void TestCustomMap3()
        {
            const string map = "[F][█][█]\n" +
                               "[█][█][█]\n" +
                               "[█][█][F]\n\r\n";
                          
            
            var cells = new[]
            {
                new BoardCell { Position = new Vector2(0, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.FLAGGED},
                new BoardCell { Position = new Vector2(1, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(0, 2), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 2), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 2), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(0, 3), IsMine = true, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 3), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 3), IsMine = false, NeighbouringCells = 0, Status = CellStatus.FLAGGED},
            };
            
            var board = new Board(1, new Vector2(3, 3), cells);
            using var sw = new StringWriter();
            Console.SetOut(sw);

            
            var boardPrinter = new ConsoleBoardPrinter();
            boardPrinter.PrintBoard(board);
            
            Assert.AreEqual(map, sw.ToString());
        }
    }
}