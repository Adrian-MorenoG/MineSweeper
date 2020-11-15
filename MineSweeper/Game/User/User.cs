using System;

namespace MineSweeper.Game.User
{
    public class User
    {
        protected string _name;

        public virtual void SetName()
        {
            throw new NotImplementedException();
        }

        public virtual string MakeAPlay()
        {
            throw new NotImplementedException();
        }
        
        public string GetName()
        {
            return _name;
        }
    }
}