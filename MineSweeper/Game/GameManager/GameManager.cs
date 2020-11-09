using System;
using MineSweeper.Game.BoardManager;
using MineSweeper.Game.Models;
using MineSweeper.Game.Printer;

namespace MineSweeper.Game.GameManager
{
    public interface IGameManager
    {
        public void Start(BoardOptions boardOptions);
        public void ProcessAction(Action action);
    }

    public class GameManager: IGameManager
    {
        private readonly IBoardManager _boardManager;
        private readonly IBoardPrinter _boardPrinter;
        private readonly IBoardGenerator _boardGenerator;

        public GameManager(IBoardPrinter boardPrinter, IBoardManager boardManager, IBoardGenerator boardGenerator)
        {
            _boardPrinter = boardPrinter;
            _boardManager = boardManager;
            _boardGenerator = boardGenerator;
        }

        public void Start(BoardOptions boardOptions)
        {
            throw new System.NotImplementedException();
        }

        public void ProcessAction(Action action)
        {
            throw new System.NotImplementedException();
        }
    }
}