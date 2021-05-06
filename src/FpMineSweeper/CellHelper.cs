namespace FpMineSweeper
{
    public static class CellHelper
    {
        public static Cell Click(this Cell cell) => cell with { IsCovered = false };
    }
}
