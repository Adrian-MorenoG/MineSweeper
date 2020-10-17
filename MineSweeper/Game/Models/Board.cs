using System;
using System.Linq;
using System.Numerics;

namespace MineSweeper.Game.Models
{
    public class Board
    {
        private readonly BoardCell[] Cells;
        public readonly int MineNumber;
        public readonly Vector2 Size;

        public Board(int mineNumber, Vector2 size, BoardCell[] cells)
        {
            MineNumber = mineNumber;
            Size = size;
            Cells = cells;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Board) obj);
        }
        
        private bool Equals(Board other)
        {
            return Cells.SequenceEqual(other.Cells) && MineNumber == other.MineNumber && Size.Equals(other.Size);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Cells, MineNumber, Size);
        }
    }
}