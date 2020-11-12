using System.Linq;
using System.Numerics;
using MineSweeper.Game;
using MineSweeper.Game.Models;
using MineSweeper.Game.Printer;
using NUnit.Framework;

namespace MineSweeper.Tests.UnitTests
{
    [TestFixture]
    public class BoardGeneratorTest
    {
        private BoardGenerator _boardGenerator;
        
        [SetUp]
        public void SetUp()
        {
            _boardGenerator = new BoardGenerator();    
        }
        
        [Test]
        public void TestCorrectBoardSize()
        {
            var boardSize = new Vector2(10, 10);
            var boardOptions = new BoardOptions(boardSize, 10, 10);
            
            var board = _boardGenerator.GenerateBoard(boardOptions);
            Assert.AreEqual(boardSize, board.Size);
        }

        [Test]
        public void TestCorrectMineNumber()
        {
            var boardSize = new Vector2(10, 10);
            var boardOptions = new BoardOptions(boardSize, 10);
            
            var board = _boardGenerator.GenerateBoard(boardOptions);
            
            Assert.AreEqual(10, board.MineNumber);
        }

        [Test]
        public void TestSameSeedSameBoard()
        {
            const int seed = 123456789;
            var generator = new BoardGenerator();
            var boardSize = new Vector2(10, 10);
            
            var board = generator.GenerateBoard(new BoardOptions(boardSize, 10, seed));
            
            Assert.AreEqual(board, generator.GenerateBoard(new BoardOptions(boardSize, 10, seed)));
        }
        
        [Test]
        public void TestCorrectMineNumberInCells()
        {
            const int mines = 100;
            var boardSize = new Vector2(20, 20);
            
            var board = _boardGenerator.GenerateBoard(new BoardOptions(boardSize, mines));
            
            var minesFound = board.Cells.Count(boardCell => boardCell.IsMine);
            Assert.AreEqual(mines, minesFound);
        }
    }
}