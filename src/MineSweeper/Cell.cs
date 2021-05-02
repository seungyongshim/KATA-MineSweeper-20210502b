using System;

namespace MineSweeper
{
    public class Cell
    {
        public bool IsBomb { get; private set; }
        public int NearBombsCount { get; set; }

        public void SetBomb()
        {
            IsBomb = true;
        }

        public override string ToString()
        {
            if (IsBomb) return "*";
            return NearBombsCount.ToString();
        }
    }
}
