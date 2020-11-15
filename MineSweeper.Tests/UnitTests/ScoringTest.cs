using System.Numerics;
using MineSweeper.Game;
using MineSweeper.Game.BoardManager;
using MineSweeper.Game.GameManager;
using MineSweeper.Game.GameManager.Actions;
using MineSweeper.Game.Models;
using MineSweeper.Game.Printer;
using MineSweeper.Game.Scoring;
using MineSweeper.Game.User;
using MineSweeper.Tests.Mocks;
using NUnit.Framework;

namespace MineSweeper.Tests.UnitTests
{
    [TestFixture]
    public class ScoringTest
    {
        private IBoardManager _boardManager;
        private IBoardPrinter _boardPrinter;
        private IBoardGenerator _boardGenerator;
        private IActionParser _actionParser;
        
        [SetUp]
        public void SetUp()
        {
            _boardManager = new BoardManager();
            _boardPrinter = new ConsoleBoardPrinter();
            _boardGenerator = new BoardGenerator();
            _actionParser = new ActionParser();
        }
        
        [Test]
        // Test if scoring tables are filled correctly
        public void ScoringOptionsTest()
        {
            BoardOptions options = new BoardOptions(new Vector2(3), 3);
            
            User user = new MockUser();
            user.SetName();
            
            GameManager gameManager = new MockGameManager(_boardPrinter, _boardManager, _boardGenerator, _actionParser);
            ScoringOptions opt = new ScoringOptions(_boardGenerator.GenerateBoard(options), user, gameManager);
            
            string expected = "Melon [BoardSize: 3*3; #Mines: 3; Win: True; Time: 10s]";
            Assert.AreEqual(expected, opt.generateRow());
            
            options = new BoardOptions(new Vector2(4), 2);
            opt = new ScoringOptions(_boardGenerator.GenerateBoard(options), user, gameManager);
            user.SetName();
            expected = "Melonazo [BoardSize: 4*4; #Mines: 2; Win: True; Time: 10s]";
            Assert.AreEqual(expected, opt.generateRow());
        }
    }
}