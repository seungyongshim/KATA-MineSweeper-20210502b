namespace FpMineSweeper
{
    public record Cell((int X, int Y) Pos, bool IsBomb, int NearBombsCount = 0, bool IsCovered = true);
}
