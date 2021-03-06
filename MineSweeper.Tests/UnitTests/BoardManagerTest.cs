﻿using System.Linq;
using System.Numerics;
using MineSweeper.Game.BoardManager;
using MineSweeper.Game.Models;
using NUnit.Framework;

namespace MineSweeper.Tests.UnitTests
{
    [TestFixture]
    public class BoardManagerTest
    {
        private BoardManager _boardManager;
        
        [SetUp]
        public void SetUp()
        {
            _boardManager = new BoardManager();    
        }
        
        /// <summary>
        /// Test that the manager throws an exception when the user tries to select an invalid
        /// position inside the board.
        /// Valid position is >= 0 && < board size.
        /// </summary>
        [Test]
        public void TestInvalidPositionThrowsException()
        {
            var cells = new[]
            {
                new BoardCell { Position = new Vector2(0, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.VISIBLE},
            };
            
            var board = new Board(1, new Vector2(1, 1), cells);
            
            Assert.Throws<InvalidBoardPositionException>(() => _boardManager.SelectCell(board, new Vector2(1, 1)));
            Assert.Throws<InvalidBoardPositionException>(() => _boardManager.SelectCell(board, new Vector2(-1, -1)));
        }

        /// <summary>
        /// Test that the manager throws an exception when the user tries to select an already visible
        /// cell.
        /// </summary>
        [Test]
        public void TestVisibleCellThrowsException()
        {
            var cells = new[]
            {
                new BoardCell { Position = new Vector2(0, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.VISIBLE},
            };
            
            var board = new Board(1, new Vector2(1, 1), cells);
            
            Assert.Throws<CellAlreadyVisibleException>(() => _boardManager.SelectCell(board, new Vector2(0, 0)));
        }
        
        /// <summary>
        /// Test that the manager throws an exception when the user selects a cell that contains a mine and that
        /// all mines in the board are marked as visible.
        /// </summary>
        [Test]
        public void TestMinedCellThrowsExceptionAndShowAllMines()
        {
            var cells = new[]
            {
                new BoardCell { Position = new Vector2(0, 0), IsMine = true, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
            };
            
            var board = new Board(1, new Vector2(1, 1), cells);
            
            Assert.Throws<MineFoundException>(() => _boardManager.SelectCell(board, new Vector2(0, 0)));
            Assert.True(board.Cells.Where(c => c.IsMine).All(c => c.Status == CellStatus.EXPLODED));
        }
        
        /// <summary>
        /// Test that the manager throws an exception when the user selects a cell that contains a mine and that
        /// all mines in the board are marked as visible.
        /// </summary>
        [Test]
        public void TestMinedCellThrowsExceptionAndShowAllMines2()
        {
            var cells = new[]
            {
                new BoardCell { Position = new Vector2(0, 0), IsMine = true, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 0), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 0), IsMine = true, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
            };
            
            var board = new Board(1, new Vector2(3, 1), cells);
            
            Assert.Throws<MineFoundException>(() => _boardManager.SelectCell(board, new Vector2(0, 0)));
            Assert.True(board.Cells.Where(c => c.IsMine).All(c => c.Status == CellStatus.EXPLODED));
        }

        /// <summary>
        /// Test that the manager marks the selected cell and all the neighbouring cells recursively that does not have
        /// mined neighbouring cells as Visible.
        /// </summary>
        [Test]
        public void TestValidPositionChangeCellsToVisibleState()
        {
            // [█][█][█]
            // [█][█][█]
            // [█ = *][█][█]
            var initialCells = new[]
            {
                new BoardCell { Position = new Vector2(0, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(0, 1), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 1), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 1), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(0, 2), IsMine = true, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 2), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 2), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
            };
            
            // [░][░][░]
            // [1][1][░]
            // [█][1][░]
            var expectedCells = new[]
            {
                new BoardCell { Position = new Vector2(0, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(1, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(2, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(0, 1), IsMine = false, NeighbouringCells = 1, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(1, 1), IsMine = false, NeighbouringCells = 1, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(2, 1), IsMine = false, NeighbouringCells = 0, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(0, 2), IsMine = true, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 2), IsMine = false, NeighbouringCells = 1, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(2, 2), IsMine = false, NeighbouringCells = 0, Status = CellStatus.VISIBLE},
            };
            
            var board = new Board(1, new Vector2(3, 3), initialCells);
       
            _boardManager.SelectCell(board, new Vector2(0, 0));
            
            Assert.AreEqual(expectedCells, board.Cells);
        }
        
        /// <summary>
        /// Test that the manager marks the selected cell and all the neighbouring cells recursively that does not have
        /// mined neighbouring cells as Visible.
        /// </summary>
        [Test]
        public void TestValidPositionChangeCellsToVisibleState2()
        {
            // [█][█][█]
            // [█][█][█]
            // [█ = *][█][█ = *]
            var initialCells = new[]
            {
                new BoardCell { Position = new Vector2(0, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(0, 1), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 1), IsMine = false, NeighbouringCells = 2, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 1), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(0, 2), IsMine = true, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 2), IsMine = false, NeighbouringCells = 2, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 2), IsMine = true, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
            };
            
            // [░][░][░]
            // [1][2][1]
            // [█][█][█]
            var expectedCells = new[]
            {
                new BoardCell { Position = new Vector2(0, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(1, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(2, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(0, 1), IsMine = false, NeighbouringCells = 1, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(1, 1), IsMine = false, NeighbouringCells = 2, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(2, 1), IsMine = false, NeighbouringCells = 1, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(0, 2), IsMine = true, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 2), IsMine = false, NeighbouringCells = 2, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 2), IsMine = true, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
            };
            
            var board = new Board(1, new Vector2(3, 3), initialCells);
       
            _boardManager.SelectCell(board, new Vector2(0, 0));
            
            Assert.AreEqual(expectedCells, board.Cells);
        }

        /// <summary>
        /// Test that manager flags a cell correctly
        /// </summary>
        [Test]
        public void TestFlagCell()
        {
            // [█][█][█]
            // [█][█][█]
            // [█][█][█]
            var initialCells = new[]
            {
                new BoardCell { Position = new Vector2(0, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(0, 1), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 1), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 1), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(0, 2), IsMine = true, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 2), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 2), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
            };
            
            // [█][█][█]
            // [█][█][█]
            // [█][█][█]
            var expectedCells = new[]
            {
                new BoardCell { Position = new Vector2(0, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(0, 1), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 1), IsMine = false, NeighbouringCells = 1, Status = CellStatus.FLAGGED},
                new BoardCell { Position = new Vector2(2, 1), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(0, 2), IsMine = true, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 2), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 2), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
            };
            
            var board = new Board(1, new Vector2(3, 3), initialCells);
            
            _boardManager.FlagCell(board, new Vector2(1, 1));
            
            Assert.AreEqual(expectedCells, board.Cells);
        }
        
        /// <summary>
        /// Test that the manager throws an exception when the user tries to select an invalid
        /// position inside the board.
        /// Valid position is >= 0 && < board size.
        /// </summary>
        [Test]
        public void TestFlagCellInvalidPositionThrowsException()
        {
            var cells = new[]
            {
                new BoardCell { Position = new Vector2(0, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.VISIBLE},
            };
            
            var board = new Board(1, new Vector2(1, 1), cells);
            
            Assert.Throws<InvalidBoardPositionException>(() => _boardManager.FlagCell(board, new Vector2(1, 1)));
            Assert.Throws<InvalidBoardPositionException>(() => _boardManager.FlagCell(board, new Vector2(-1, -1)));
        }
        
        /// <summary>
        /// Test that manager flags a cell correctly
        /// </summary>
        [Test]
        public void TestFlagVisibleCellThrowsException()
        {
            // [█][█][█]
            // [█][█][█]
            // [█][█][█]
            var initialCells = new[]
            {
                new BoardCell { Position = new Vector2(0, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 0), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(0, 1), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 1), IsMine = false, NeighbouringCells = 1, Status = CellStatus.VISIBLE},
                new BoardCell { Position = new Vector2(2, 1), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(0, 2), IsMine = true, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(1, 2), IsMine = false, NeighbouringCells = 1, Status = CellStatus.HIDDEN},
                new BoardCell { Position = new Vector2(2, 2), IsMine = false, NeighbouringCells = 0, Status = CellStatus.HIDDEN},
            };

            var board = new Board(1, new Vector2(3, 3), initialCells);
            
            Assert.Throws<CellAlreadyVisibleException>(() => _boardManager.FlagCell(board, new Vector2(1, 1)));
        }
    }
}