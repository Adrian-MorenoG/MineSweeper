using System;

namespace MineSweeper
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
    }

    public class Board
    {
        private readonly BoardCell[] Cells;
        public readonly int MineNumber;
        public readonly int Size;
        public readonly int SideSize;

        public Board(int mineNumber, int size, BoardCell[] cells)
        {
            MineNumber = mineNumber;
            SideSize = size;
            Size = size * size;
            Cells = cells;
        }

    }

    public class BoardOptions
    {
        private readonly int Mines;
        private readonly long Seed;
        private readonly int Size;

        public BoardOptions(int size, int mines, long seed)
        {
            Size = size;
            Mines = mines;
            Seed = seed;
        }

        public BoardOptions(int size, int mines) : this(size, mines, Environment.TickCount)
        {
            
        }
    }

    public interface IBoardGenerator
    {
        Board GenerateBoard(BoardOptions options);
    }

    public class BoardGenerator : IBoardGenerator
    {
        public Board GenerateBoard(BoardOptions options)
        {
            throw new NotImplementedException();
        }
    }
}