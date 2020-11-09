using System;
using MineSweeper.Game.BoardManager;
using MineSweeper.Game.Printer;
using Action = MineSweeper.Game.GameManager.Actions.Action;

namespace MineSweeper.Game.GameManager
{
    public interface IGameManager
    {
        public void Start();
        public void ProcessAction(Action action);
    }

    class GameManager: IGameManager
    {
        private readonly IBoardManager _boardManager;
        private readonly IBoardPrinter _boardPrinter;

        public GameManager(IBoardPrinter boardPrinter, IBoardManager boardManager)
        {
            _boardPrinter = boardPrinter;
            _boardManager = boardManager;
        }

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void ProcessAction(Action action)
        {
            throw new System.NotImplementedException();
        }
    }
}