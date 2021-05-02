using System;

namespace MineSweeper
{
    public class Cell
    {
        public bool IsBomb { get; private set; }
        public int NearBombsCount { get; set; }
        public bool IsCovered { get; private set; } = true;

        public void SetBomb()
        {
            IsBomb = true;
        }

        public override string ToString()
        {
            if (IsCovered) return ".";
            if (IsBomb) return "*";
            return NearBombsCount.ToString();
        }

        public void Click()
        {
            IsCovered = false;
        }
    }
}
