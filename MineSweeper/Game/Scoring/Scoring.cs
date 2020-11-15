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

        public void Prepare()
        {
            string path = Directory.GetCurrentDirectory();;
            _path = Path.Combine(path, "scoring.txt");
            _sw = File.Exists(_path) ? File.CreateText(_path) : File.AppendText(_path);
        }
        
        public virtual void AddRow(ScoringOptions row)
        {
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