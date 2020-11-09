using System.Numerics;

namespace MineSweeper.Game.GameManager.Actions
{
    public class FlagCellAction: Action
    {
        public readonly Vector2 CellPosition;

        public FlagCellAction(Vector2 cellPosition)
        {
            CellPosition = cellPosition;
        }
    }
}