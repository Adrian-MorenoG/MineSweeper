using System;
using MineSweeper.Game.Models;

namespace MineSweeper.Game.GameManager
{
    public class MineFoundException: Exception
    {
        private BoardCell cell;

        public MineFoundException(BoardCell cell): base($"You selected a mined cell")
        {
            this.cell = cell;
        }
    }
}