using System;

namespace MineSweeper.Game.Models
{
    public class BoardCell
    {
        public readonly bool IsMine;
        public readonly int NeighbouringCells;
        public readonly int X;
        public readonly int Y;

        public BoardCell(int x, int y, bool isMine, int neighbouringCells)
        {
            X = x;
            Y = y;
            IsMine = isMine;
            NeighbouringCells = neighbouringCells;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((BoardCell) obj);
        }
        
        private bool Equals(BoardCell other)
        {
            return IsMine == other.IsMine && NeighbouringCells == other.NeighbouringCells && X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsMine, NeighbouringCells, X, Y);
        }
    }
}