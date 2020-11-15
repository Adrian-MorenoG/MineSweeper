using System.Numerics;
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
        public override void Start(BoardOptions boardOptions)
        {
            BoardCell[] cells = new BoardCell[(int) (boardOptions.Size.X*boardOptions.Size.Y)];
            for (int i = 0; i < (int) (boardOptions.Size.X * boardOptions.Size.Y); i++)
            {
                BoardCell c = new BoardCell { IsMine = false, NeighbouringCells = 0, Position = new Vector2(), Status = CellStatus.VISIBLE };
                
            }
            _board = new Board(boardOptions.Mines, boardOptions.Size, cells);
        }

        public void ProcessAction(Action action)
        {
            throw new System.NotImplementedException();
        }

        public override string GetElapsedTime()
        {
            return "10s";
        }

        public override bool UserWin()
        {
            return true;
        }

        public MockGameManager(IBoardPrinter boardPrinter, IBoardManager boardManager, IBoardGenerator boardGenerator, IActionParser actionParser) : base(boardPrinter, boardManager, boardGenerator, actionParser)
        {
            
        }
    }
}