using System.Numerics;

namespace MineSweeper.Game.GameManager.Actions
{
    public class SelectCellAction: Action
    {
        public readonly Vector2 CellPosition;

        public SelectCellAction(Vector2 cellPosition)
        {
            CellPosition = cellPosition;
        }
    }
}