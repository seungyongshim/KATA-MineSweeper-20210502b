namespace FpMineSweeper
{
    public static partial class Prelude
    {
        public static string str(Cell cell) => cell switch
        {
            { IsCovered : true } => ".",
            { IsBomb : true} => "*",
            var x => x.NearBombsCount.ToString()
        };
    }
}
