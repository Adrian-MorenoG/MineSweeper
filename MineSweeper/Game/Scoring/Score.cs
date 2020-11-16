using System;
using System.Numerics;
using MineSweeper.Game.Models;

namespace MineSweeper.Game.Scoring
{
    public sealed class Score
    {
        private readonly string _name;
        private readonly int _mineNumber;
        private readonly bool _userWon;
        private readonly long _timeSpent;
        private readonly Vector2 _boardSize;

        public Score(string name, int mineNumber, bool userWon, long timeSpent, Vector2 boardSize)
        {
            _name = name;
            _mineNumber = mineNumber;
            _userWon = userWon;
            _timeSpent = timeSpent;
            _boardSize = boardSize;
        }

        public static Score GenerateScore(TimeSpan elapsedTime, bool userWon,  User.IUser user, Board board)
        {
            return new Score(
                user.GetName(),
                board.MineNumber,
                userWon,
                elapsedTime.Seconds,
                board.Size
            );
        }

        public override string ToString()
        {
            // UserName [BoardSize: Y*Z; #Mines: X; Win: true/false; Time: Seconds]
            return $"{_name} " +
                   $"[BoardSize: {_boardSize.X}*{_boardSize.Y}; " +
                   $"#Mines: {_mineNumber}; " +
                   $"Win: {_userWon}; " +
                   $"Time: {_timeSpent}s]";
        }
    }
}