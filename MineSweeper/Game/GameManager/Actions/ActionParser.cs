﻿namespace MineSweeper.Game.GameManager.Actions
{
    public interface IActionParser
    {
        Action ParseAction(string input);
    }

    public class ActionParser: IActionParser 
    {
        public Action ParseAction(string input)
        {
            throw new System.NotImplementedException();
        }
    }
}