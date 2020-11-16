using System;
using System.Linq;
using System.Text;
using MineSweeper.Game.BoardManager;
using MineSweeper.Game.GameManager.Actions;
using MineSweeper.Game.Models;
using MineSweeper.Game.Printer;
using MineSweeper.Game.Scoring;
using Action = MineSweeper.Game.GameManager.Actions.Action;

namespace MineSweeper.Game.GameManager
{
    public interface IGameManager
    {
        public void Start(BoardOptions boardOptions);
        public void ProcessAction(Action action);

        public bool IsRunning();
    }

    public class GameManager: IGameManager
    {
        private readonly IBoardManager _boardManager;
        private readonly IBoardPrinter _boardPrinter;
        private readonly IBoardGenerator _boardGenerator;
        private readonly IActionParser _actionParser;
        private readonly IScoreManager _scoreManager;
        private readonly string _menuOptions;
        private readonly User.User _user;
        
        private bool _gameFinished;
        
        private Board _board;

        public GameManager(
            IBoardPrinter boardPrinter, 
            IBoardManager boardManager, 
            IBoardGenerator boardGenerator,
            IActionParser actionParser,
            IScoreManager scoreManager,
            User.User user)
        {
            _boardPrinter = boardPrinter;
            _boardManager = boardManager;
            _boardGenerator = boardGenerator;
            _actionParser = actionParser;
            _scoreManager = scoreManager;
            _user = user;

            _menuOptions = new StringBuilder()
                .AppendLine("--------------------------")
                .AppendLine("SELECT AN OPTION:")
                .AppendLine("S X Y - Select a cell in the x,y position")
                .AppendLine("F X Y - Flag a cell in the x,y position")
                .AppendLine("E - Exit the game")
                .AppendLine()
                .ToString();
        }

        public void Start(BoardOptions boardOptions)
        {
            _gameFinished = false;
            _board = _boardGenerator.GenerateBoard(boardOptions);
            _user.ReadUserName();

            var gameStart = DateTime.UtcNow;

            while (!_gameFinished)
            {
                try
                {
                    // Clear the screen
                    MineSweeperConsole.Clear();
                    
                    // Print the board
                    _boardPrinter.PrintBoardWithCoords(_board);

                    // Print the available options
                    MineSweeperConsole.WriteLine(_menuOptions);

                    // Get the user input
                    var input = _user.ReadUserAction();
                    MineSweeperConsole.WriteLine(input);

                    // Parse the action
                    var action = _actionParser.ParseAction(input);

                    // Process the action
                    ProcessAction(action);
                }
                catch (MineFoundException)
                {
                    _gameFinished = true;
                    
                    // Clear the screen
                    MineSweeperConsole.Clear();

                    // Print the board
                    _boardPrinter.PrintBoardWithCoords(_board);
                    MineSweeperConsole.WriteLine("YOU LOOSE");
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            
            var elapsedTime = DateTime.UtcNow - gameStart;
            var score = Score.GenerateScore(TimeSpan.FromSeconds(elapsedTime.TotalSeconds), UserWon(), _user, _board);
            _scoreManager.AddRow(score);
            _scoreManager.PrintScore();
        }

        public void ProcessAction(Action action)
        {
            switch (action)
            {
                case FinishGameAction _:
                    _gameFinished = true;
                    break;
                case SelectCellAction selectCellAction:
                    _boardManager.SelectCell(_board, selectCellAction.CellPosition);

                    // All the cells that are not mines has been selected. We WON.
                    if (UserWon())
                    {
                        _gameFinished = true;
                        _boardPrinter.PrintBoardWithCoords(_board);
                        MineSweeperConsole.WriteLine("YOU WON");
                    }
                    
                    break;
                case FlagCellAction flagCellAction:
                    _boardManager.FlagCell(_board, flagCellAction.CellPosition);
                    break;
            }
        }

        public bool IsRunning()
        {
            return !_gameFinished;
        }

        private bool UserWon()
        {
            return _board.Cells.Count(c => !c.IsMine && c.Status == CellStatus.VISIBLE) ==
                   _board.Cells.Length - _board.MineNumber;
        }
    }
}