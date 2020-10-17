using System;
using System.IO;
using System.Numerics;
using MineSweeper.Game;
using MineSweeper.Game.Models;
using MineSweeper.Game.Printer;
using NUnit.Framework;

namespace MineSweeper.Tests
{
    [TestFixture]
    public class ConsoleBoardPrinterTest
    {
        [Test]
        public void TestSimpleMap()
        {
            const int seed = 17102020;
            const string map = "[0][0][0]\n" +
                               "[0][1][1]\n" +
                               "[0][1][*]\n\r\n";
            
            var boardOptions = new BoardOptions(new Vector2(3, 3), 1, seed);
            var board = new BoardGenerator().GenerateBoard(boardOptions);
            using var sw = new StringWriter();
            Console.SetOut(sw);
            
            var boardPrinter = new ConsoleBoardPrinter();
            boardPrinter.PrintBoard(board);
            
            Assert.AreEqual(map, sw.ToString());
        }
        
        [Test]
        public void TestBigMap()
        {
            const int seed = 17102020;
            const string map = "[1][*][4][*][4][3][3][3][4][*]\n" +
                               "[2][3][*][*][*][*][*][*][*][*]\n" +
                               "[*][2][4][*][*][7][6][*][5][*]\n" +
                               "[2][2][3][*][*][*][*][4][4][3]\n"+
                               "[*][2][4][*][8][*][*][4][*][*]\n" + 
                               "[3][*][3][*][*][*][4][*][5][*]\n" + 
                               "[*][3][4][4][6][4][3][2][*][2]\n" +
                               "[4][*][5][*][*][*][3][4][3][2]\n" +
                               "[*][*][*][*][6][5][*][*][*][3]\n" + 
                               "[2][3][3][3][*][*][4][*][*][*]\n\r\n";
            
            var boardOptions = new BoardOptions(new Vector2(10, 10), 50, seed);
            var board = new BoardGenerator().GenerateBoard(boardOptions);
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var boardPrinter = new ConsoleBoardPrinter();
            boardPrinter.PrintBoard(board);
            
            Assert.AreEqual(map, sw.ToString());
        }
    }
}