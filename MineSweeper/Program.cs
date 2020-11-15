using System.Numerics;
using MineSweeper.Game;
using MineSweeper.Game.BoardManager;
using MineSweeper.Game.GameManager;
using MineSweeper.Game.GameManager.Actions;
using MineSweeper.Game.Models;
using MineSweeper.Game.Printer;
using MineSweeper.Game.User;

namespace MineSweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            var boardPrinter = new ConsoleBoardPrinter();
            var boardManager = new BoardManager();
            var boardGenerator = new BoardGenerator();
            var actionParser = new ActionParser();
            var user = new User();
            var gameManager = new GameManager(boardPrinter, boardManager, boardGenerator, actionParser, user);
            var boardOptions = new BoardOptions(new Vector2(5, 5), 5);
            gameManager.Start(boardOptions);
        }
    }
}