using System.IO;
using System.Numerics;
using MineSweeper.Game.Printer;

namespace MineSweeper.Tests
{
    /// <summary>
    /// Mocked console that writes and reads from custom provided string readers.
    /// </summary>
    public class MockConsole: IConsoleWrapper
    {
        private readonly StringReader _input;
        
        private readonly StringWriter _output;

        public MockConsole(StringReader input, StringWriter output)
        {
            _input = input;
            _output = output;
        }

        public void WriteLine(string value)
        {
            _output.WriteLine(value);
        }

        public string ReadLine()
        {
            return _input.ReadLine();
        }

        public Vector2 GetCursorPosition()
        {
            throw new System.NotImplementedException();
        }

        public void SetCursorPosition(Vector2 pos)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}