using System.Numerics;
using MineSweeper.Game;
using MineSweeper.Game.Models;
using NUnit.Framework;

namespace MineSweeper.Tests
{
    [TestFixture]
    public class BoardGeneratorTest
    {
        [Test]
        public void TestCorrectBoardSize()
        {
            var boardSize = new Vector2(10, 10);
            var boardOptions = new BoardOptions(boardSize, 10, 10);
            var board = new BoardGenerator().GenerateBoard(boardOptions);
            Assert.AreEqual(boardSize, board.Size);
        }

        [Test]
        public void TestCorrectMineNumber()
        {
            var boardSize = new Vector2(10, 10);
            var boardOptions = new BoardOptions(boardSize, 10);
            var board = new BoardGenerator().GenerateBoard(boardOptions);
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
    }
}