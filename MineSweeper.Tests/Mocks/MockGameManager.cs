using MineSweeper.Game;
using MineSweeper.Game.BoardManager;
using MineSweeper.Game.GameManager;
using MineSweeper.Game.GameManager.Actions;
using MineSweeper.Game.Models;
using MineSweeper.Game.Printer;

namespace MineSweeper.Tests.Mocks
{
    public class MockGameManager: GameManager
    {
        public void Start(BoardOptions boardOptions)
        {
            throw new System.NotImplementedException();
        }

        public void ProcessAction(Action action)
        {
            throw new System.NotImplementedException();
        }

        public string GetElapsedTime()
        {
            throw new System.NotImplementedException();
        }

        public bool UserWin()
        {
            throw new System.NotImplementedException();
        }

        public MockGameManager(IBoardPrinter boardPrinter, IBoardManager boardManager, IBoardGenerator boardGenerator, IActionParser actionParser) : base(boardPrinter, boardManager, boardGenerator, actionParser)
        {
            
        }
    }
}