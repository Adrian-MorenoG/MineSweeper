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
            ValidatePosition(board, position);
            var cellPos = position.X + (position.Y * board.Size.X);
            var cell = board.Cells[(int) cellPos];

            if (cell.Status == CellStatus.VISIBLE)
            {
                throw new CellAlreadyVisibleException(cell);
            }

            if (cell.IsMine)
            {
                throw new MineFoundException(cell);
            }
            
            var validNeighbourPositions = new[]
            {
                new Vector2(-1, -1),
                new Vector2(0, -1),
                new Vector2(1, -1),
                new Vector2(-1, 0),
                new Vector2(1, 0),
                new Vector2(-1, 1),
                new Vector2(0, 1),
                new Vector2(1, 1)
            };
            
            
        }

        private static void ValidatePosition(Board board, Vector2 position)
        {
            if (position.X < 0 || position.X > board.Size.X - 1)
            {
                throw new InvalidBoardPositionException(position);
            }

            if (position.Y < 0 || position.Y > board.Size.Y - 1)
            {
                throw new InvalidBoardPositionException(position);
            }
        }
    }
}