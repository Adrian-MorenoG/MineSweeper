using System;
using System.IO;
using System.Numerics;
using MineSweeper.Game;
using MineSweeper.Game.BoardManager;
using MineSweeper.Game.GameManager;
using MineSweeper.Game.Models;
using MineSweeper.Game.Printer;
using NUnit.Framework;

namespace MineSweeper.Tests
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
            _gameManager = new GameManager(boardPrinter, boardManager, boardGenerator);
        }

        [Test]
        public void TestGameLoop()
        {
            // SEED: 170023000
            /*
              [1][1][░][░][░]
              [*][1][░][1][1]
              [1][1][░][1][*]
              [░][░][░][2][2]
              [░][░][░][1][*]
            */
            
            var boardOptions = new BoardOptions(new Vector2(5, 5), 3, 170023000);
            
            _gameManager.Start(boardOptions);
            
            // Now the game is in a loop waiting for the console input of the user.
            
        }

    }
}