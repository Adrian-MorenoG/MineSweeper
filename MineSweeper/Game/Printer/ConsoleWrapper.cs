﻿using System;

namespace MineSweeper.Game.Printer
{
    public interface IConsoleWrapper
    {
        void WriteLine(string value);
        string ReadLine();
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
    }
}