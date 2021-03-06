﻿using System.Linq;
using System.Numerics;
using MineSweeper.Game.Models;

namespace MineSweeper.Game.BoardManager
{
    public interface IBoardManager
    {
        void SelectCell(Board board, Vector2 position);
        void FlagCell(Board board, Vector2 position);
    }

    public class BoardManager: IBoardManager
    {
        private static readonly Vector2[] ValidMovements =
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
                MarkAllMinesAsVisible(board);
                throw new MineFoundException(cell);
            }
            
            OpenCell(board, cell);
        }
        
        public void FlagCell(Board board, Vector2 position)
        {
            ValidatePosition(board, position);
            var cellPos = position.X + (position.Y * board.Size.X);
            var cell = board.Cells[(int) cellPos];

            if (cell.Status == CellStatus.VISIBLE)
            {
                throw new CellAlreadyVisibleException(cell);
            }

            cell.Status = CellStatus.FLAGGED;
        }

        private void OpenCell(Board board, BoardCell cell)
        {
            if (cell.IsMine || cell.Status != CellStatus.HIDDEN)
            {
                return;
            }

            if (cell.Status != CellStatus.FLAGGED)
            {
                cell.Status = CellStatus.VISIBLE;
            }

            if (cell.NeighbouringCells > 0)
            {
                return;
            }
            
            foreach (var neighbour in ValidMovements)
            {
                try
                {
                    Vector2 pos = cell.Position + neighbour;
                    ValidatePosition(board, pos);
                    var neighborBoardPosition = (int) (pos.X + pos.Y * board.Size.X);
                    BoardCell neighbourCell = board.Cells[neighborBoardPosition];
                    OpenCell(board, neighbourCell);
                }
                catch (InvalidBoardPositionException)
                {
                    // Out of board position
                }
            }
        }

        private void MarkAllMinesAsVisible(Board board)
        {
            foreach (var cell in board.Cells.Where(c => c.IsMine))
            {
                cell.Status = CellStatus.EXPLODED;
            }
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