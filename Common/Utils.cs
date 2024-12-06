namespace AOC
{
    public static class Utils
    {
        public static bool IsInBounds<T>(IEnumerable<IEnumerable<T>> space, int x, int y)
        {
            return x >= 0 && x < space.Count() && y >= 0 && y < space.First().Count();
        }
    }
}