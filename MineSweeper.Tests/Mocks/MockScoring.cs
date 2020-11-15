using System;
using System.Collections;
using MineSweeper.Game.Printer;
using MineSweeper.Game.Scoring;

namespace MineSweeper.Tests.Mocks
{
    public class MockScoring : Scoring
    {
        private ArrayList Rows;
        
        public MockScoring()
        {
            Rows = new ArrayList();
        }
        public override void AddRow(ScoringOptions row)
        {
            Rows.Add(row.generateRow());
        }

        public override void DeleteRow(int pos)
        {
            Rows.RemoveAt(pos);
        }

        public override void PrintScoring()
        {
            foreach (var r in Rows)
            {
                MineSweeperConsole.WriteLine((string) r);
            }
        }

        public override int Length()
        {
            return Rows.Count;
        }
    }
}