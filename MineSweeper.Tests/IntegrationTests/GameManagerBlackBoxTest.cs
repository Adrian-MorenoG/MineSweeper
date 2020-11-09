using System;
using System.IO;
using System.Numerics;
using System.Text;
using MineSweeper.Game;
using MineSweeper.Game.BoardManager;
using MineSweeper.Game.GameManager;
using MineSweeper.Game.GameManager.Actions;
using MineSweeper.Game.Models;
using MineSweeper.Game.Printer;
using MineSweeper.Tests.Mocks;
using NUnit.Framework;

namespace MineSweeper.Tests.IntegrationTests
{
    [TestFixture]
    public class GameManagerBlackBoxTest
    {
        private GameManager _gameManager;

        [SetUp]
        public void SetUp()
        {
            var boardPrinter = new ConsoleBoardPrinter();
            var boardManager = new BoardManager();
            var boardGenerator = new BoardGenerator();
            var actionParser = new ActionParser();
            _gameManager = new GameManager(boardPrinter, boardManager, boardGenerator, actionParser);
        }

        [Test]
        public void TestGameLoop1()
        {
            /*
              [1][1][░][░][░]
              [*][1][░][1][1]
              [1][1][░][1][*]
              [░][░][░][2][2]
              [░][░][░][1][*]
            */
            
            // EXPECTED:
            var expected = File.ReadAllText("game_manager_output_1.txt").Replace("\r", "");
            var boardOptions = new BoardOptions(new Vector2(5, 5), 3, 170023000);

            // All the commands that the user will send.
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("S 0 0");
            stringBuilder.AppendLine("S 1 0");
            stringBuilder.AppendLine("S 2 0");
            stringBuilder.AppendLine("F 0 1");
            stringBuilder.AppendLine("F 4 4");
            stringBuilder.AppendLine("F 4 2");
            stringBuilder.AppendLine("S 4 3");

            var input = new StringReader(stringBuilder.ToString());
            
            // All the output that the console prints.
            var output = new StringWriter();
            var mockedConsole = new MockConsole(input, output);
            MineSweeperConsole.SetConsoleWrapper(mockedConsole);
            
            _gameManager.Start(boardOptions);
            
            Console.WriteLine(output.ToString().Replace("\r", ""));
            
            Assert.AreEqual(expected, output.ToString().Replace("\r", ""));
            Assert.False(_gameManager.IsRunning());
        }

        [Test]
        public void TestExitGame()
        {
            /*
              [1][1][░][░][░]
              [*][1][░][1][1]
              [1][1][░][1][*]
              [░][░][░][2][2]
              [░][░][░][1][*]
            */
            
            // EXPECTED:
            var expected = File.ReadAllText("game_manager_output_2.txt").Replace("\r", "");
            var boardOptions = new BoardOptions(new Vector2(5, 5), 3, 170023000);

            // All the commands that the user will send.
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("S 0 0");
            stringBuilder.AppendLine("S 1 0");
            stringBuilder.AppendLine("S 2 0");
            stringBuilder.AppendLine("F 0 1");
            stringBuilder.AppendLine("E");

            var input = new StringReader(stringBuilder.ToString());
            
            // All the output that the console prints.
            var output = new StringWriter();
            var mockedConsole = new MockConsole(input, output);
            MineSweeperConsole.SetConsoleWrapper(mockedConsole);
            
            _gameManager.Start(boardOptions);
            
            Console.WriteLine(output.ToString().Replace("\r", ""));
            
            Assert.AreEqual(expected, output.ToString().Replace("\r", ""));
            Assert.False(_gameManager.IsRunning());
        }
        
        [Test]
        public void TestSelectMine()
        {
            /*
              [1][1][░][░][░]
              [*][1][░][1][1]
              [1][1][░][1][*]
              [░][░][░][2][2]
              [░][░][░][1][*]
            */
            
            // EXPECTED:
            var expected = File.ReadAllText("game_manager_output_3.txt").Replace("\r", "");
            var boardOptions = new BoardOptions(new Vector2(5, 5), 3, 170023000);

            // All the commands that the user will send.
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("S 0 1");
 
            var input = new StringReader(stringBuilder.ToString());
            
            // All the output that the console prints.
            var output = new StringWriter();
            var mockedConsole = new MockConsole(input, output);
            MineSweeperConsole.SetConsoleWrapper(mockedConsole);
            
            _gameManager.Start(boardOptions);
            
            Console.WriteLine(output.ToString().Replace("\r", ""));
            
            Assert.AreEqual(expected, output.ToString().Replace("\r", ""));
            Assert.False(_gameManager.IsRunning());
        }
        
        [Test]
        public void TestSelectAlreadyVisibleCell()
        {
            /*
              [1][1][░][░][░]
              [*][1][░][1][1]
              [1][1][░][1][*]
              [░][░][░][2][2]
              [░][░][░][1][*]
            */
            
            // EXPECTED:
            var expected = File.ReadAllText("game_manager_output_4.txt").Replace("\r", "");
            var boardOptions = new BoardOptions(new Vector2(5, 5), 3, 170023000);

            // All the commands that the user will send.
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("S 0 0");
            stringBuilder.AppendLine("S 0 0");
            stringBuilder.AppendLine("E");
 
            var input = new StringReader(stringBuilder.ToString());
            
            // All the output that the console prints.
            var output = new StringWriter();
            var mockedConsole = new MockConsole(input, output);
            MineSweeperConsole.SetConsoleWrapper(mockedConsole);
            
            _gameManager.Start(boardOptions);
            
            Console.WriteLine(output.ToString().Replace("\r", ""));
            
            Assert.AreEqual(expected, output.ToString().Replace("\r", ""));
            Assert.False(_gameManager.IsRunning());
        }
    }
}