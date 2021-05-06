namespace FpMineSweeper
{
    public record Cell(bool IsBomb, int NearBombsCount = 0, bool IsCovered = true);
}
