using MineSweeper.Game;
using MineSweeper.Game.BoardManager;
using MineSweeper.Game.GameManager;
using MineSweeper.Game.GameManager.Actions;
using MineSweeper.Game.Models;
using MineSweeper.Game.Printer;
using MineSweeper.Game.Scoring;
using MineSweeper.Game.User;
using MineSweeper.Tests.Mocks;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;

namespace MineSweeper.Automation
{
    public class Tests
    {
        public enum AutomationTestType
        {
            CheckAllMinePositions,
            CheckVisibleWhenSelect,
            WinInOneMoveAllPositions,
            LoseInOneMoveAllPositions
        }

        private static string FILENAMEALLPOSITIONS = "AllPositions.txt";

        private GameManager _gameManager;
        private BoardManager _boardManager;
        private BoardGenerator _boardGenerator;
        private BoardOptions _boardOptions;

        private Board _board;
        private ActionParser _actionParser;

        private Logger _logger;

        [SetUp]
        public void Setup()
        {
            _logger = new Logger();

            var user = new User();

            var boardPrinter = new ConsoleBoardPrinter();
            var scoreManager = new ScoreManager();

            _actionParser = new ActionParser();
            _boardManager = new BoardManager();
            _boardGenerator = new BoardGenerator();

            // Delete all the scores at the start of each test.
            scoreManager.DeleteAll();    

            //Setups the game
            _gameManager = new GameManager(boardPrinter, _boardManager, _boardGenerator, _actionParser, scoreManager, user);

            _boardOptions = new BoardOptions(new Vector2(5, 5), 5);
            
            _board = _boardGenerator.GenerateBoard(_boardOptions);
            
            // Generate all positions file
            FileGenerator.GenerateAllPositions(_board);
        }

        [Test]
        public void CheckAllMinePositions()
        {
            // All the positions of the board are mines.
            foreach (BoardCell boardCell in _board.Cells)
                boardCell.IsMine = true;

            using StreamReader sr = File.OpenText(FILENAMEALLPOSITIONS);
            string instruction = string.Empty;

            while ((instruction = sr.ReadLine()) != null)
            {
                // Parse the action
                SelectCellAction action = (SelectCellAction)_actionParser.ParseAction(instruction);

                try
                {
                    _boardManager.SelectCell(_board, action.CellPosition);
                }
                catch (System.Exception ex)
                {
                    try
                    {
                        Assert.AreEqual(typeof(MineFoundException), ex.GetType());
                        _logger.Log("Mine selected", AutomationTestType.CheckAllMinePositions);
                    }
                    catch (System.Exception)
                    {
                        _logger.Log("ASSERT ERROR: Mine not selected", AutomationTestType.CheckAllMinePositions);
                    }
                }
            }
        }

        [Test]
        public void CheckVisibleWhenSelect()
        {
            // All the positions of the board are not mines.
            foreach (BoardCell boardCell in _board.Cells)
                boardCell.IsMine = false;

            using StreamReader sr = File.OpenText(FILENAMEALLPOSITIONS);
            string instruction = string.Empty;

            while ((instruction = sr.ReadLine()) != null)
            {
                // Parse the action
                SelectCellAction action = (SelectCellAction)_actionParser.ParseAction(instruction);

                try
                {
                    _boardManager.SelectCell(_board, action.CellPosition);
                }
                catch (CellAlreadyVisibleException)
                {
                    //We ignore this kind of exception.
                }

                BoardCell cell = _board.Cells.FirstOrDefault(x => x.Position == action.CellPosition);

                try
                {
                    Assert.AreEqual(CellStatus.VISIBLE, cell.Status);
                    _logger.Log(string.Format("Cell {0} is Visible", instruction), AutomationTestType.CheckVisibleWhenSelect);
                }
                catch (System.Exception)
                {
                    _logger.Log(string.Format("ASSERT ERROR: Cell {0} is NOT visible", instruction), AutomationTestType.CheckVisibleWhenSelect);
                }
            }           
        }

        [Test]
        public void WinInOneMoveAllPositions()
        {
            _boardOptions = new BoardOptions(new Vector2(5, 5), 24);

            using StreamReader sr = File.OpenText(FILENAMEALLPOSITIONS);
            string instruction = string.Empty;

            while ((instruction = sr.ReadLine()) != null)
            {
                _board = _boardGenerator.GenerateBoard(_boardOptions);

                // All the positions of the board are mines.
                foreach (BoardCell boardCell in _board.Cells)
                    boardCell.IsMine = true;

                // Parse the action
                SelectCellAction action = (SelectCellAction)_actionParser.ParseAction(instruction);
                BoardCell cell = _board.Cells.FirstOrDefault(x => x.Position == action.CellPosition);

                //The required cell is not a mine
                cell.IsMine = false;

                _boardManager.SelectCell(_board, action.CellPosition);

                bool result =  _board.Cells.Count(c => !c.IsMine && c.Status == CellStatus.VISIBLE) ==
                               _board.Cells.Length - _board.MineNumber;
               
                try
                {
                    Assert.AreEqual(true, result);
                    _logger.Log(string.Format("Win the game in one move {0}", instruction), AutomationTestType.WinInOneMoveAllPositions);
                }
                catch (System.Exception)
                {
                    _logger.Log(string.Format("ASSERT ERROR: We don't win the game in one move {0}", instruction), AutomationTestType.WinInOneMoveAllPositions);
                }
            }
        }

        [Test]
        public void LoseInOneMoveAllPositions()
        {
            _boardOptions = new BoardOptions(new Vector2(5, 5), 24);

            using StreamReader sr = File.OpenText(FILENAMEALLPOSITIONS);
            string instruction = string.Empty;

            while ((instruction = sr.ReadLine()) != null)
            {
                _board = _boardGenerator.GenerateBoard(_boardOptions);

                // All the positions of the board are mines.
                foreach (BoardCell boardCell in _board.Cells)
                    boardCell.IsMine = false;

                // Parse the action
                SelectCellAction action = (SelectCellAction)_actionParser.ParseAction(instruction);
                BoardCell cell = _board.Cells.FirstOrDefault(x => x.Position == action.CellPosition);

                //The required cell is not a mine
                cell.IsMine = true;

                try
                {
                    _boardManager.SelectCell(_board, action.CellPosition);
                }
                catch (System.Exception ex)
                {
                    try
                    {
                        Assert.AreEqual(typeof(MineFoundException), ex.GetType());
                        _logger.Log(string.Format("We lose the game in one move at {0}", instruction), AutomationTestType.LoseInOneMoveAllPositions);
                    }
                    catch (System.Exception)
                    {
                        _logger.Log(string.Format("ASSERT ERROR: We don't lose the game in one move at {0}", instruction), AutomationTestType.LoseInOneMoveAllPositions);
                    }
                }
            }
        }
    }
}