using System;
using System.Collections.Generic;
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
        private readonly Vector2[] _cellsToOpen =
        {
            new Vector2(-1, -1),   // Arriba izquierda
            new Vector2(0, -1),    // Arriba
            new Vector2(1, -1),    // Derecha arriba
            new Vector2(-1, 0),    // Izquierda
            new Vector2(1, 0),     // Derecha
            new Vector2(-1, 1),    // Izquierda abajo
            new Vector2(0, 1),     // Abajo
            new Vector2(1, 1)      // Derecha abajo
        };
        
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
                cell.Status = CellStatus.EXPLODED;
                throw new MineFoundException(cell);
            }
            
            OpenCell(board, cell);
        }

        private void OpenCell(Board board, BoardCell cell)
        {
            if (cell.IsMine || cell.Status != CellStatus.HIDDEN)
            {
                return;
            }

            cell.Status = CellStatus.VISIBLE;

            if (cell.NeighbouringCells > 0)
            {
                return;
            }
            
            foreach (var neighbour in _cellsToOpen)
            {
                try
                {
                    Vector2 pos = cell.Position + neighbour;
                    ValidatePosition(board, pos);
                    var neighborBoardPosition = (int) (pos.X + pos.Y * board.Size.X);
                    BoardCell neighbourCell = board.Cells[neighborBoardPosition];
                    OpenCell(board, neighbourCell);
                }
                catch (InvalidBoardPositionException _)
                {
                    // Out of board position
                }
            }
            
        }

        public void FlagCell(Board board, Vector2 position)
        {
            throw new NotImplementedException();
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