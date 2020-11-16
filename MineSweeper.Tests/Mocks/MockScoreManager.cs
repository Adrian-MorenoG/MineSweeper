using System.Collections.Generic;
using MineSweeper.Game.Printer;
using MineSweeper.Game.Scoring;

namespace MineSweeper.Tests.Mocks
{
    public class MockScoreManager : IScoreManager
    {
        private readonly List<Score> _scores;

        public MockScoreManager()
        {
            _scores = new List<Score>();
        }

        public void AddRow(Score row)
        {
            _scores.Add(row);
        }

        public void DeleteAll()
        {
            _scores.Clear();
        }

        public void PrintScore()
        {
            foreach (var score in _scores)
            {
                MineSweeperConsole.WriteLine(score.ToString());
            }
        }

        public string GetScorePath()
        {
            return "test_path";
        }
    }
}