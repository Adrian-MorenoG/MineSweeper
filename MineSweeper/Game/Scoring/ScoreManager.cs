using System.IO;
using MineSweeper.Game.Printer;

namespace MineSweeper.Game.Scoring
{
    public interface IScoreManager
    {
        public void AddRow(Score row);

        public void DeleteAll();

        public void PrintScore();

        public string GetScorePath();
    }
    
    public class ScoreManager: IScoreManager
    {
        private readonly string _path;

        public ScoreManager()
        {
            _path = Path.Combine(Directory.GetCurrentDirectory(), "scoring.txt");
        }

        public void AddRow(Score row)
        {
            if (!File.Exists(_path))
            {
                using var sw = File.CreateText(_path);
                sw.WriteLine(row.ToString());
            }
            else
            {
                using var sw = File.AppendText(_path);
                sw.WriteLine(row.ToString());
            }
        }

        public void DeleteAll()
        {
            File.Delete(_path);
        }

        public void PrintScore()
        {
            if (!File.Exists(_path)) return;
            using var sr = File.OpenText(_path);
            
            MineSweeperConsole.WriteLine("\n\n::: SCOREBOARD :::");
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                MineSweeperConsole.WriteLine(s);
            }
        }

        public string GetScorePath()
        {
            return _path;
        }
    }
}