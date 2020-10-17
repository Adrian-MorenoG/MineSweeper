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

        /// Prints a representation of this cell.
        /// Where hidden cells are marked as [ ].
        /// Where flagged cells are marked as [F].
        /// Where unhidden cells are marked as [-] if no cells are neighbouring them, or the number of neighbouring cells.
        /// Where cells that already exploded ar marked as [*].
        public override string ToString()
        {
            return Status switch
            {
                CellStatus.HIDDEN => "█",
                CellStatus.VISIBLE => NeighbouringCells > 0 ? NeighbouringCells.ToString() : "░",
                CellStatus.FLAGGED => "F",
                CellStatus.EXPLODED => "*",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}