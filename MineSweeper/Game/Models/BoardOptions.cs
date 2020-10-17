using System;
using System.Numerics;

namespace MineSweeper.Game.Models
{
    public class BoardOptions
    {
        public readonly int Mines;
        public readonly int Seed;
        public readonly Vector2 Size;

        public BoardOptions(Vector2 size, int mines, int seed)
        {
            Size = size;
            Mines = mines;
            Seed = seed;
        }

        public BoardOptions(Vector2 size, int mines) : this(size, mines, Environment.TickCount)
        {
            
        }
    }
}