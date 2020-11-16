using MineSweeper.Game.User;

namespace MineSweeper.Tests.Mocks
{
    public class MockUser : IUser
    {
        private readonly string[] _plays =
        {
            "S 0 0",
            "F 0 1",
            "S 1 1",
            "S 2 2"
        };

        private int _playCounter;
        private readonly string _name;
        
        public MockUser(string name)
        {
            _playCounter = 0;
            _name = name;

        }

        public void ReadUserName()
        {
            // Nothing to do.
        }

        public string ReadUserAction()
        {
            var index = _playCounter;
            _playCounter++;
            return _plays[index];
        }

        public string GetName()
        {
            return _name;
        }
    }
}