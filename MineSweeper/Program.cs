using System.Numerics;
using MineSweeper.Game;
using MineSweeper.Game.BoardManager;
using MineSweeper.Game.GameManager;
using MineSweeper.Game.GameManager.Actions;
using MineSweeper.Game.Models;
using MineSweeper.Game.Printer;
using MineSweeper.Game.Scoring;
using MineSweeper.Game.User;

namespace MineSweeper
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var user = new User();
            var boardPrinter = new ConsoleBoardPrinter();
            var boardManager = new BoardManager();
            var boardGenerator = new BoardGenerator();
            var actionParser = new ActionParser();
            var scoreManager = new ScoreManager();
            var gameManager = new GameManager(boardPrinter, boardManager, boardGenerator, actionParser, scoreManager, user);
            var boardOptions = new BoardOptions(new Vector2(5, 5), 5);
            gameManager.Start(boardOptions);
        }
    }
}