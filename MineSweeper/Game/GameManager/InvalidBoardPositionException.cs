using System;
using System.Numerics;

namespace MineSweeper.Game.GameManager
{
    public class InvalidBoardPositionException: Exception
    {
        private Vector2 position;

        public InvalidBoardPositionException(Vector2 position): base($"This position is invalid: {position}")
        {
            this.position = position;
        }
    }
}