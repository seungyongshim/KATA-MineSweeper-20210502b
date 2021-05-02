namespace MineSweeper
{
    public class Cell
    {
        public bool IsBomb { get; private set; }
        public int NearBombsCount { get; set; }
        public bool IsCovered { get; private set; } = true;

        public void SetBomb() => IsBomb = true;

        
        public void Click() => IsCovered = false;

        public override string ToString() => this switch
        {
            { IsCovered: true } => ".",
            { IsBomb: true} => "*",
            _ => NearBombsCount.ToString(),
        };
    }
}
