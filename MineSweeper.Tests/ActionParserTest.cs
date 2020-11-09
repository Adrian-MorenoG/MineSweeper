using System;
using System.Numerics;
using MineSweeper.Game.GameManager.Actions;
using NUnit.Framework;

namespace MineSweeper.Tests
{
    [TestFixture]
    public class ActionParserTest
    {
        private IActionParser _actionParser;
        
        [SetUp]
        public void SetUp()
        {
            _actionParser = new ActionParser();    
        }
        
        [Test]
        public void TestSelectCellParsedCorrectly()
        {
            const string input = "S 10 10";
            
            var action = _actionParser.ParseAction(input);
            
            Assert.IsInstanceOf<SelectCellAction>(action);
            Assert.AreEqual(new Vector2(10, 10), ((SelectCellAction) action).CellPosition);
        }
        
        [Test]
        public void TestSelectCellParsedIncorrectly()
        {
            const string input = "S 10 10s";
            
            Assert.Throws<ArgumentException>(() => _actionParser.ParseAction(input));
        }
        
        [Test]
        public void TestSelectCellParsedIncorrectly2()
        {
            const string input = "S 10 10 15";
            
            Assert.Throws<ArgumentException>(() => _actionParser.ParseAction(input));
        }
        
           
        [Test]
        public void TestSelectCellParsedIncorrectly3()
        {
            const string input = "S 10-10";
            
            Assert.Throws<ArgumentException>(() => _actionParser.ParseAction(input));
        }
    }
}