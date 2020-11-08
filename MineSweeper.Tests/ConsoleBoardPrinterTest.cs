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
        private String _clr;
        private ConsoleBoardPrinter _printer;

        [SetUp]
        public void SetUp()
        {
            bool isMac = Environment.OSVersion.Platform.Equals(PlatformID.MacOSX);
            bool isUnix = Environment.OSVersion.Platform.Equals(PlatformID.Unix);
            _clr = (isMac || isUnix) ? "" : "\r";
            _printer = new ConsoleBoardPrinter();
        }
        
        /// <summary>
        /// Tests that a map with all the cells hidden is printed
        /// correctly.
        /// </summary>
        [Test]
        public void TestHiddenMap()
        {
            string map = "[█][█][█]\n" + 
                         "[█][█][█]\n" + 
                         $"[█][█][█]\n{_clr}\n";

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
            
            _printer.PrintBoard(board);
            
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
            string map = "[░][░][░]\n" +
                         "[1][1][░]\n" +
                         $"[█][1][░]\n{_clr}\n";
                          
            
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

            _printer.PrintBoard(board);
            
            Assert.AreEqual(map, sw.ToString());
        }
        
        /// <summary>
        /// Test that a map with different cell status is printed correctly.
        /// The only cell that contains a mine is the [0, 3] and has exploded.
        /// All the other cells are hidden.
        /// </summary>
        [Test]
        public void TestCustomMap2()
        {
            string map = "[█][█][█]\n" + 
                         "[█][█][█]\n" + 
                         $"[*][█][█]\n{_clr}\n";
                          
            
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
            
            _printer.PrintBoard(board);
            
            Assert.AreEqual(map, sw.ToString());
        }
         
        /// <summary>
        /// Test that a map with different cell status is printed correctly.
        /// All the cells are hidden except the [0, 0] and [2, 2] that are flagged.
        /// </summary>
        [Test]
        public void TestCustomMap3()
        {
            string map = "[F][█][█]\n" +
                         "[█][█][█]\n" + 
                         $"[█][█][F]\n{_clr}\n";
                          
            
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
            
            _printer.PrintBoard(board);
            
            Assert.AreEqual(map, sw.ToString());
        }

        /// <summary>
        /// Like TestHiddenMap but with coords
        /// </summary>
        /// <returns></returns>
        [Test]
        public void TestHiddenMapWithCoords()
        {
            string map = "    0  1  2 \n" +
                         "   ---------\n" +
                         "0| [█][█][█]\n" +
                         "1| [█][█][█]\n" +
                         $"2| [█][█][█]\n{_clr}\n";
            
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
            
            _printer.PrintBoardWithCoords(board);
            Assert.AreEqual(map, sw.ToString());
        }
        
        /// <summary>
        /// Like TestCustomMap but with coords
        /// </summary>
        /// <returns></returns>
        [Test]
        public void TestCustomMapWithCoords()
        {
            string map = "    0  1  2 \n" +
                         "   ---------\n" +
                         "0| [░][░][░]\n" +
                         "1| [1][1][░]\n" +
                         $"2| [█][1][░]\n{_clr}\n";
            
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
            
            _printer.PrintBoardWithCoords(board);
            Assert.AreEqual(map, sw.ToString());
        }
        
        /// <summary>
        /// Like TestCustomMap2 but with coords
        /// </summary>
        /// <returns></returns>
        [Test]
        public void TestCustomMap2WithCoords()
        {
            string map = "    0  1  2 \n" +
                         "   ---------\n" +
                         "0| [█][█][█]\n" +
                         "1| [█][█][█]\n" +
                         $"2| [*][█][█]\n{_clr}\n";
            
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
            
            _printer.PrintBoardWithCoords(board);
            Assert.AreEqual(map, sw.ToString());
        }
        
        /// <summary>
        /// Like TestCustomMap3 but with coords
        /// </summary>
        /// <returns></returns>
        [Test]
        public void TestCustomMap3WithCoords()
        {
            string map = "    0  1  2 \n" +
                         "   ---------\n" +
                         "0| [F][█][█]\n" +
                         "1| [█][█][█]\n" +
                         $"2| [█][█][F]\n{_clr}\n";
            
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
            
            _printer.PrintBoardWithCoords(board);
            Assert.AreEqual(map, sw.ToString());
        }
    }
}