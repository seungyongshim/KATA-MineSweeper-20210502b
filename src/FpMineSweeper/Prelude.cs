using System.Linq;

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

        public static Cell cell(bool isBomb, int nearBombsCount = 0, bool isCovered = true) =>
            new Cell(isBomb, nearBombsCount, isCovered);

        public static MineFieldInit mineField(int width, int height) =>
            new MineFieldInit(width, height);
    }
}
