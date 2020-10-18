using System;
using System.Numerics;
using MineSweeper.Game.Models;

namespace MineSweeper.Game
{
    public class CellAlreadyVisibleException: Exception
    {
        private BoardCell cell;

        public CellAlreadyVisibleException(BoardCell cell): base($"This cell is already visible")
        {
            this.cell = cell;
        }
    }
    
    public class MineFoundException: Exception
    {
        private BoardCell cell;

        public MineFoundException(BoardCell cell): base($"You selected a mined cell")
        {
            this.cell = cell;
        }
    }
    
    public class InvalidBoardPositionException: Exception
    {
        private Vector2 position;

        public InvalidBoardPositionException(Vector2 position): base($"This position is invalid: {position}")
        {
            this.position = position;
        }
    }
    
    public interface IGameManager
    {
        void SelectCell(Board board, Vector2 position);
    }

    public class GameManager: IGameManager
    {
        public void SelectCell(Board board, Vector2 position)
        {
            throw new System.NotImplementedException();
        }
    }
}