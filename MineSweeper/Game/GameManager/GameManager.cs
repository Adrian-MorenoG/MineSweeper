using System;
using System.Linq;
using System.Threading;
using MineSweeper.Game.BoardManager;
using MineSweeper.Game.GameManager.Actions;
using MineSweeper.Game.Models;
using MineSweeper.Game.Printer;
using Action = MineSweeper.Game.GameManager.Actions.Action;

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
        private readonly IActionParser _actionParser;

        private bool _gameFinished;
        private Board _board;

        public GameManager(
            IBoardPrinter boardPrinter, 
            IBoardManager boardManager, 
            IBoardGenerator boardGenerator,
            IActionParser actionParser)
        {
            _boardPrinter = boardPrinter;
            _boardManager = boardManager;
            _boardGenerator = boardGenerator;
            _actionParser = actionParser;
        }

        public void Start(BoardOptions boardOptions)
        {
            _gameFinished = false;
            _board = _boardGenerator.GenerateBoard(boardOptions);
            
            while (!_gameFinished)
            {
                MineSweeperConsole.Clear();

                _boardPrinter.PrintBoardWithCoords(_board);
                MineSweeperConsole.WriteLine("\n\n");
                MineSweeperConsole.WriteLine("SELECT AN ACTION");
                MineSweeperConsole.WriteLine("\n\n");

                var input = MineSweeperConsole.ReadLine();
                var action = _actionParser.ParseAction(input);

                try
                {
                    ProcessAction(action);
                }
                catch (Exception e)
                {
                    // ignored
                }
            }
        }

        public void ProcessAction(Action action)
        {
            switch (action)
            {
                case FinishGameAction _:
                    _gameFinished = true;
                    MineSweeperConsole.WriteLine("GAME FINISHED");
                    break;
                case SelectCellAction selectCellAction:
                    _boardManager.SelectCell(_board, selectCellAction.CellPosition);

                    // All the cells that are not mines has been selected. We WON.
                    if (_board.Cells.Count(c => !c.IsMine && c.Status == CellStatus.VISIBLE) == _board.Cells.Length - _board.MineNumber)
                    {
                        _gameFinished = true;
                        _boardPrinter.PrintBoardWithCoords(_board);
                        MineSweeperConsole.WriteLine("YOU WON");
                    }
                    
                    break;
                case FlagCellAction flagCellAction:
                    _boardManager.FlagCell(_board, flagCellAction.CellPosition);
                    break;
                case RestartGameAction _:
                    _gameFinished = true;
                    MineSweeperConsole.WriteLine("GAME RESTARTED");
                    break;
            }
        }

        public bool IsRunning()
        {
            return !_gameFinished;
        }
    }
}