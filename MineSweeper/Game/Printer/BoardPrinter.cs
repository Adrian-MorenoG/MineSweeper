using MineSweeper.Game.Models;

namespace MineSweeper.Game.Printer
{
    public interface IBoardPrinter
    {
        void PrintBoard(Board board);
    }
}