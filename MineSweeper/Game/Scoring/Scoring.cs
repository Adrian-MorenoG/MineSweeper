using System;
using System.Collections;
using System.IO;
using MineSweeper.Game.Printer;

namespace MineSweeper.Game.Scoring
{
    public class Scoring
    {
        private string _path;

        public void Prepare()
        {
            string path = Directory.GetCurrentDirectory();;
            _path = Path.Combine(path, "scoring.txt");
        }
        
        public virtual void AddRow(ScoringOptions row)
        {
            if (!File.Exists(_path))
            {
                using (StreamWriter sw = File.CreateText(_path))
                {
                    sw.WriteLine(row.generateRow());
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(_path))
                {
                    sw.WriteLine(row.generateRow());
                }
            }
        }

        public virtual void DeleteRow(int pos)
        {
            var tmp = File.ReadAllLines(_path);
            var lines = new string[tmp.Length-1];
            for (int i = 0; i < tmp.Length; i++)
            {
                if (i == pos) continue;
                lines[i] = tmp[i];
            }
            
            File.Delete(_path);
            File.Create(_path);
            using (StreamWriter w = File.CreateText(_path))
            {
                foreach (var l in lines)
                {
                    w.WriteLine(l);
                }
            }
        }

        public virtual void PrintScoring()
        {
            if (File.Exists(_path))
            {
                using (StreamReader sr = File.OpenText(_path))
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        MineSweeperConsole.WriteLine(s);
                    }
                }
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