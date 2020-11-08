using System.Linq;
using System.Numerics;

namespace MineSweeper.Game.Models
{
    public sealed class Board
     {
         public readonly BoardCell[] Cells;
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
             var other = (Board) obj;
             return Cells.SequenceEqual(other.Cells) && MineNumber == other.MineNumber && Size.Equals(other.Size);
         }
     }
 }