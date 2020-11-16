using System;
using System.IO;
using System.Numerics;
using MineSweeper.Game.Models;
using MineSweeper.Game.Printer;
using MineSweeper.Game.Scoring;
using MineSweeper.Tests.Mocks;
using NUnit.Framework;

namespace MineSweeper.Tests.UnitTests
{
    [TestFixture]
    public class ScoreManagerTest
    {
        private static readonly bool IsWindows = Environment.OSVersion.Platform.Equals(PlatformID.Win32NT);
        private static readonly string Clr = IsWindows ? "\r" : "";
        
        private ScoreManager _scoreManager;
  
        [SetUp]
        public void SetUp()
        {
            _scoreManager = new ScoreManager();
            _scoreManager.DeleteAll();
        }
        
        [Test]
        public void TestAfterConstructionPathIsValid()
        {
            var expectedPath = Path.Combine(Directory.GetCurrentDirectory(), "scoring.txt");
            var scoreManager = new ScoreManager();
            Assert.AreEqual(expectedPath, scoreManager.GetScorePath()); 
        }
        
        [Test]
        public void TestPrintScoreWhenFileNotExistsDoesNotThrowExceptionJustReturns()
        {
            var scoreManager = new ScoreManager();
            File.Delete(scoreManager.GetScorePath());
            Assert.DoesNotThrow(() => scoreManager.PrintScore());
        }
        
         
        [Test]
        public void TestAddScoreSuccess()
        {
            var user = new MockUser("Test User");
            var board = new Board(1, Vector2.One, new BoardCell[2]);

            var input = new StringReader("");
            var output = new StringWriter();
            var consoleMock = new MockConsole(input, output);
            MineSweeperConsole.SetConsoleWrapper(consoleMock);
            var scoreManager = new ScoreManager();
            
            // Test when file does not exists.
            scoreManager.AddRow(Score.GenerateScore(TimeSpan.Zero, false, user, board));
            
            // Test when file already exists.
            scoreManager.AddRow(Score.GenerateScore(TimeSpan.FromSeconds(10), true, user, board));
            
            Assert.DoesNotThrow(() => scoreManager.PrintScore());
            Assert.AreEqual($"\n\n::: SCOREBOARD :::{Clr}\nTest User [BoardSize: 1*1; #Mines: 1; Win: False; Time: 0s]{Clr}\n" +
                            $"Test User [BoardSize: 1*1; #Mines: 1; Win: True; Time: 10s]{Clr}\n",output.ToString());
        }
    }
}