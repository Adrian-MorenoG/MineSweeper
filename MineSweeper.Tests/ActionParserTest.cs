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
        public void TestEmptyInputThrowsInvalidActionException()
        {
            const string input = "";
            
            Assert.Throws<InvalidActionException>(() => _actionParser.ParseAction(input));
        }
        
        [Test]
        public void TestBlankInputThrowsInvalidActionException()
        {
            const string input = " ";
            
            Assert.Throws<InvalidActionException>(() => _actionParser.ParseAction(input));
        }
        
        [Test]
        public void TestNullInputThrowsInvalidActionException()
        {
            const string input = null;
            
            Assert.Throws<InvalidActionException>(() => _actionParser.ParseAction(input));
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
        
        [Test]
        public void TestFlagCellParsedCorrectly()
        {
            const string input = "F 5 2";
            
            var action = _actionParser.ParseAction(input);
            
            Assert.IsInstanceOf<FlagCellAction>(action);
            Assert.AreEqual(new Vector2(5, 2), ((FlagCellAction) action).CellPosition);
        }
        
        [Test]
        public void TestFlagCellParsedIncorrectly()
        {
            const string input = "F 5s 3s";
            
            Assert.Throws<ArgumentException>(() => _actionParser.ParseAction(input));
        }
        
        [Test]
        public void TestFlagCellParsedIncorrectly2()
        {
            const string input = "F 10 10 15";
            
            Assert.Throws<ArgumentException>(() => _actionParser.ParseAction(input));
        }
        
           
        [Test]
        public void TestFlagCellParsedIncorrectly3()
        {
            const string input = "F 10-10";
            
            Assert.Throws<ArgumentException>(() => _actionParser.ParseAction(input));
        }
        
        [Test]
        public void TestFinishGameParsedCorrectly()
        {
            const string input = "E";
            
            var action = _actionParser.ParseAction(input);
            
            Assert.IsInstanceOf<FinishGameAction>(action);
        }
        
        [Test]
        public void TestFinishGameParsedICorrectly()
        {
            const string input = "E 5";
            
            Assert.Throws<ArgumentException>(() => _actionParser.ParseAction(input));
        }
    }
}