namespace AOC
{
    public static class Utils
    {
        public static bool IsInBounds<T>(IEnumerable<IEnumerable<T>> space, int x, int y)
        {
            return IsInRange2d(x, y, space.Count(), space.First().Count());
        }

        public static bool IsInRange2d(int x, int y, int maxX, int maxY, int minX = 0, int minY = 0)
        {
            return x >= minX && x < maxX && y >= minY && y < maxY;
        }
    }
}