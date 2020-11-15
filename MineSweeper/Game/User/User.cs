using System;
using MineSweeper.Game.Printer;

namespace MineSweeper.Game.User
{
    public class User
    {
        protected string _name;

        public virtual void SetName()
        {
            _name = MineSweeperConsole.ReadLine();
        }

        public virtual string MakeAPlay()
        {
            return MineSweeperConsole.ReadLine();
        }
        
        public string GetName()
        {
            return _name;
        }
    }
}