using System.IO;
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
        private GameManager _gameManager;
        private Scoring _scoring;
        
        [SetUp]
        public void SetUp()
        {
            _boardManager = new BoardManager();
            _boardPrinter = new ConsoleBoardPrinter();
            _boardGenerator = new BoardGenerator();
            _actionParser = new ActionParser();
            _gameManager = new MockGameManager(_boardPrinter, _boardManager, _boardGenerator, _actionParser, new MockUser());
            _scoring = new MockScoring();
        }
        
        [Test]
        // Test if scoring tables are generated correctly
        public void ScoringOptionsTest()
        {
            BoardOptions options = new BoardOptions(new Vector2(3), 3);
            
            User user = new MockUser();
            user.SetName();
            
            ScoringOptions opt = new ScoringOptions(_boardGenerator.GenerateBoard(options), user, _gameManager);
            
            string expected = "Melon [BoardSize: 3*3; #Mines: 3; Win: True; Time: 10s]";
            Assert.AreEqual(expected, opt.generateRow());
            
            options = new BoardOptions(new Vector2(4), 2);
            opt = new ScoringOptions(_boardGenerator.GenerateBoard(options), user, _gameManager);
            user.SetName();
            expected = "Melonazo [BoardSize: 4*4; #Mines: 2; Win: True; Time: 10s]";
            Assert.AreEqual(expected, opt.generateRow());
        }

        [Test]
        public void AddRowScoringTest()
        {
            User user = new MockUser();
            user.SetName();
            
            BoardOptions options = new BoardOptions(new Vector2(3), 2);
            ScoringOptions opt = new ScoringOptions(_boardGenerator.GenerateBoard(options), user, _gameManager);
            _scoring.AddRow(opt);
            
            options = new BoardOptions(new Vector2(1), 1);
            opt = new ScoringOptions(_boardGenerator.GenerateBoard(options), user, _gameManager);
            _scoring.AddRow(opt);
            
            options = new BoardOptions(new Vector2(5, 4), 9);
            opt = new ScoringOptions(_boardGenerator.GenerateBoard(options), user, _gameManager);
            _scoring.AddRow(opt);
            
            options = new BoardOptions(new Vector2(33, 1), 8);
            opt = new ScoringOptions(_boardGenerator.GenerateBoard(options), user, _gameManager);
            _scoring.AddRow(opt);
            
            Assert.AreEqual(4, _scoring.Length());
        }

        [Test]
        public void DeleteRowScoringTest()
        {
            User user = new MockUser();
            user.SetName();
            
            BoardOptions options = new BoardOptions(new Vector2(3), 2);
            ScoringOptions opt = new ScoringOptions(_boardGenerator.GenerateBoard(options), user, _gameManager);
            _scoring.AddRow(opt);
            _scoring.AddRow(opt);
            
            _scoring.DeleteRow(0);
            _scoring.DeleteRow(0);
            
            Assert.AreEqual(0, _scoring.Length());
        }

        [Test]
        public void PrintScoringTest()
        {
            User user = new MockUser();
            user.SetName();
            
            BoardOptions options = new BoardOptions(new Vector2(3), 2);
            ScoringOptions opt = new ScoringOptions(_boardGenerator.GenerateBoard(options), user, _gameManager);
            _scoring.AddRow(opt);
            
            options = new BoardOptions(new Vector2(1), 1);
            opt = new ScoringOptions(_boardGenerator.GenerateBoard(options), user, _gameManager);
            _scoring.AddRow(opt);
            
            options = new BoardOptions(new Vector2(5, 4), 9);
            opt = new ScoringOptions(_boardGenerator.GenerateBoard(options), user, _gameManager);
            _scoring.AddRow(opt);
            
            options = new BoardOptions(new Vector2(33, 1), 8);
            opt = new ScoringOptions(_boardGenerator.GenerateBoard(options), user, _gameManager);
            _scoring.AddRow(opt);
            
            Assert.AreEqual(4, _scoring.Length());
            
            string expected = "Melon [BoardSize: 3*3; #Mines: 2; Win: True; Time: 10s]\n" +
                              "Melon [BoardSize: 1*1; #Mines: 1; Win: True; Time: 10s]\n" +
                              "Melon [BoardSize: 5*4; #Mines: 9; Win: True; Time: 10s]\n" +
                              "Melon [BoardSize: 33*1; #Mines: 8; Win: True; Time: 10s]\n";
            
            var output = new StringWriter();
            MineSweeperConsole.SetConsoleWrapper(new MockConsole(new StringReader(""), output));
            _scoring.PrintScoring();
            Assert.AreEqual(expected, output.ToString());
            
            _scoring.DeleteRow(1);
            _scoring.DeleteRow(0);
            
            expected = "Melon [BoardSize: 5*4; #Mines: 9; Win: True; Time: 10s]\n" +
                       "Melon [BoardSize: 33*1; #Mines: 8; Win: True; Time: 10s]\n";
            
            output = new StringWriter();
            MineSweeperConsole.SetConsoleWrapper(new MockConsole(new StringReader(""), output));
            _scoring.PrintScoring();
            Assert.AreEqual(2, _scoring.Length());
            Assert.AreEqual(expected, output.ToString());
        }
    }
}