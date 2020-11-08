using System;
using MineSweeper.Game.Models;

namespace MineSweeper.Game.GameManager
{
    public class CellAlreadyVisibleException: Exception
    {
        private BoardCell cell;

        public CellAlreadyVisibleException(BoardCell cell): base($"This cell is already visible")
        {
            this.cell = cell;
        }
    }
}