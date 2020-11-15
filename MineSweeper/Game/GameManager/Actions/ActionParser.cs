using System;
using System.Numerics;

namespace MineSweeper.Game.GameManager.Actions
{
    public interface IActionParser
    {
        Action ParseAction(string input);
    }
    
    public class InvalidActionException : Exception
    {
    }

    public class ActionParser: IActionParser 
    {
        public Action ParseAction(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new InvalidActionException();
            }
            
            var parts = input.Split(" ");

            switch(parts[0].ToUpper())
            {
                case "S":
                    return ParseSelectCellAction(parts);
                case "F":
                    return ParseFlagCellAction(parts);
                case "E":
                    return ParseFinishGameAction(parts);
                case "R":
                    return ParseRestartGameAction(parts);
                default: throw new InvalidActionException();
            };
        }

        private SelectCellAction ParseSelectCellAction(string[] parts)
        {
            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid action");
            }

            try
            {
                var x = Convert.ToInt32(parts[1]);
                var y = Convert.ToInt32(parts[2]);

                return new SelectCellAction(new Vector2(x, y));
            }
            catch (Exception e)
            {
                throw new ArgumentException("Invalid action");
            }
        }
        
        private FlagCellAction ParseFlagCellAction(string[] parts)
        {
            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid action");
            }

            try
            {
                var x = Convert.ToInt32(parts[1]);
                var y = Convert.ToInt32(parts[2]);

                return new FlagCellAction(new Vector2(x, y));
            }
            catch (Exception e)
            {
                throw new ArgumentException("Invalid action");
            }
        }
        
        private FinishGameAction ParseFinishGameAction(string[] parts)
        {
            if (parts.Length != 1)
            {
                throw new ArgumentException("Invalid action");
            }

            return new FinishGameAction();
        }
        
        private Action ParseRestartGameAction(string[] parts)
        {
            if (parts.Length != 1)
            {
                throw new ArgumentException("Invalid action");
            }

            return new RestartGameAction();
        }
    }
}