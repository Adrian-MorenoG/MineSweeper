using System;
using System.Numerics;

namespace MineSweeper.Game.Models
{
    public enum CellStatus
    {
        HIDDEN,
        VISIBLE,
        FLAGGED,
        EXPLODED
    }

    public sealed class BoardCell
    {
        public bool IsMine;
        public int NeighbouringCells;
        public Vector2 Position;
        public CellStatus Status;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            var other = (BoardCell) obj;
            return IsMine == other.IsMine && NeighbouringCells == other.NeighbouringCells &&
                   Position.Equals(other.Position) && Status == other.Status;
        }

        public override string ToString()
        {
            return IsMine ? "*" : NeighbouringCells.ToString();
        }
    }
}