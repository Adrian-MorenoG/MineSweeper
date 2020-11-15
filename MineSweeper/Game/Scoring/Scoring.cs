using System;
using System.Collections;

namespace MineSweeper.Game.Scoring
{
    public class Scoring
    {
        protected ArrayList Rows;
        
        public Scoring()
        {
            Rows = new ArrayList();
        }
        
        private void Prepare()
        {
            throw new NotImplementedException();
        }
        
        public virtual void AddRow(ScoringOptions row)
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteRow(int pos)
        {
            throw new NotImplementedException();
        }

        public void PrintScoring()
        {
            throw new NotImplementedException();
        }

        public int RowsCounter()
        {
            return Rows.Count;
        }
    }
}