using NUnit.Framework;

namespace MineSweeper.Tests
{
    [TestFixture]
    public class BoardGeneratorTest
    {
        [Test]
        public void TestCorrectBoardSize()
        {
            var boardOptions = new BoardOptions(10, 10);
            var board = new BoardGenerator().GenerateBoard(boardOptions);
            Assert.AreEqual(10 * 10, board.Size);
            Assert.AreEqual(10, board.SideSize);
        }

        [Test]
        public void TestCorrectMineNumber()
        {
            var boardOptions = new BoardOptions(10, 10);
            var board = new BoardGenerator().GenerateBoard(boardOptions);
            Assert.AreEqual(10, board.MineNumber);
        }

        [Test]
        public void TestSameSeedSameBoard()
        {
            const long seed = 123456789L;

            var generator = new BoardGenerator();
            var board = generator.GenerateBoard(new BoardOptions(10, 10, seed));
            Assert.AreEqual(board, generator.GenerateBoard(new BoardOptions(10, 10, seed)));
        }
    }
}