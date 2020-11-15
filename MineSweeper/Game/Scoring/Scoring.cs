using System;
using System.Collections;
using System.IO;
using MineSweeper.Game.Printer;

namespace MineSweeper.Game.Scoring
{
    public class Scoring
    {
        private StreamWriter _sw;
        private string _path;

        private void Prepare()
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            _path = Path.Combine(path, "scoring.txt");
        }
        
        public virtual void AddRow(ScoringOptions row)
        {
            _sw = new StreamWriter(_path);
            _sw.WriteLine(row.generateRow());
            _sw.Flush();
            _sw.Close();
        }

        public virtual void DeleteRow(int pos)
        {
            
        }

        public virtual void PrintScoring()
        {
            if (File.Exists(_path))
            {
                MineSweeperConsole.WriteLine(File.ReadAllText(_path));
            }
        }

        public virtual int Length()
        {
            if (File.Exists(_path))
            {
                return File.ReadAllLines(_path).Length;
            }

            return 0;
        }
    }
}