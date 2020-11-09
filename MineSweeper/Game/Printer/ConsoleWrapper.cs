using System;
using System.Numerics;

namespace MineSweeper.Game.Printer
{
    public interface IConsoleWrapper
    {
        void WriteLine(string value);
        string ReadLine();

        public Vector2 GetCursorPosition();
        
        public void SetCursorPosition(Vector2 pos);
        void Clear();
    }

    public class SystemConsole: IConsoleWrapper
    {
        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
        
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public Vector2 GetCursorPosition()
        {
            return new Vector2(Console.CursorLeft, Console.CursorTop);
        }

        public void SetCursorPosition(Vector2 pos)
        {
            Console.SetCursorPosition((int) pos.X, (int) pos.Y);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }

    public static class MineSweeperConsole
    {
        // By default use the system console wrapper. 
        // For tests, custom consoles can be provided with the SetConsoleWrapper method.
        private static IConsoleWrapper _consoleWrapper = new SystemConsole();
        
        public static void WriteLine(string value)
        {
            _consoleWrapper.WriteLine(value);
        }

        public static string ReadLine()
        {
            return _consoleWrapper.ReadLine();
        }

        public static void SetConsoleWrapper(IConsoleWrapper consoleWrapper)
        {
            _consoleWrapper = consoleWrapper;
        }

        public static Vector2 GetCursorPosition()
        {
            return _consoleWrapper.GetCursorPosition();
        }

        public static void SetCursorPosition(Vector2 pos)
        {
            _consoleWrapper.SetCursorPosition(pos);
        }

        public static void Clear()
        {
            _consoleWrapper.Clear();
        }
    }
}