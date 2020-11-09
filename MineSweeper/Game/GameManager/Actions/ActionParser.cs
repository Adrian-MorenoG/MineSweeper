using System;
using System.Numerics;

namespace MineSweeper.Game.GameManager.Actions
{
    public interface IActionParser
    {
        Action ParseAction(string input);
    }

    public class ActionParser: IActionParser 
    {
        public Action ParseAction(string input)
        {
            var parts = input.Split(" ");

            return parts[0] switch
            {
                "S" => ParseSelectCellAction(parts),
                _ => throw new ArgumentException("Invalid action")
            };
        }

        private Action ParseSelectCellAction(string[] parts)
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
    }
}