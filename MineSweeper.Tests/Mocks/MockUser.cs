using MineSweeper.Game.User;

namespace MineSweeper.Tests.Mocks
{
    public class MockUser : User
    {
        private string[] _plays =
        {
            "S 0 0",
            "F 0 1",
            "S 1 1",
            "S 2 2"
        };

        private string[] _names =
        {
            "Melon",
            "Melonazo"
        };

        private int _nameCounter;
        private int _playCounter;

        public MockUser()
        {
            _nameCounter = 0;
            _playCounter = 0;
        }

        public override void SetName()
        {
            _name = _names[_nameCounter++];
        }

        public override string MakeAPlay()
        {
            return _plays[_playCounter++];
        }
    }
}