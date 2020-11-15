using System;
using MineSweeper.Game.Models;

namespace MineSweeper.Game.Scoring
{
    public sealed class ScoringOptions
    {
        protected GameManager.GameManager _gameManager;
        protected User.User _user;
        protected Board _board;
        
        public ScoringOptions(Board board, User.User user, GameManager.GameManager gameManager)
        {
            _board = board;
            _user = user;
            _gameManager = gameManager;
        }

        public string generateRow()
        {
            // UserName [BoardSize: Y*Z; #Mines: X; Win: true/false; Time: Seconds]
            return $"{_user.GetName()} " +
                   $"[BoardSize: {_board.Size.X}*{_board.Size.Y}; " +
                   $"#Mines: {_board.MineNumber}; " +
                   $"Win: {_gameManager.UserWin()}; " +
                   $"Time: {_gameManager.GetElapsedTime()}]";
        }
    }
}