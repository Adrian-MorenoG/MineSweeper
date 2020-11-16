using System;
using MineSweeper.Game.Printer;

namespace MineSweeper.Game.User
{
    public interface IUser
    {
        public void ReadUserName();
        public string ReadUserAction();

        public string GetName();
    }
    
    public class User: IUser
    {
        protected string Name;

        public void ReadUserName()
        {
            MineSweeperConsole.WriteLine("Put your name");
            Name = MineSweeperConsole.ReadLine();
        }

        public string ReadUserAction()
        {
            return MineSweeperConsole.ReadLine();
        }
        
        public string GetName()
        {
            return Name;
        }
    }
}