using System.Security.Cryptography.X509Certificates;

namespace AOC.Y2024
{
    public class Day04 : IProblem
    {
        private const string word = "XMAS";
        private List<List<char>> grid = new List<List<char>>();

        public void Parse(string[] input)
        {
            foreach (string s in input)
            {
                grid.Add(new List<char>(s.ToCharArray()));
            }
        }

        public object PartOne()
        {
            int count = 0;

            for (int x = 0; x < grid.Count; x++)
            {
                for (int y = 0; y < grid.Count; y++)
                {
                    count += IsPresent(x, y, 0, -1, -1);
                    count += IsPresent(x, y, 0, -1, 0);
                    count += IsPresent(x, y, 0, -1, 1);
                    count += IsPresent(x, y, 0, 0, -1);
                    count += IsPresent(x, y, 0, 0, 1);
                    count += IsPresent(x, y, 0, 1, -1);
                    count += IsPresent(x, y, 0, 1, 0);
                    count += IsPresent(x, y, 0, 1, 1);
                }
            }

            return count;
        }

        public object PartTwo()
        {
            int count = 0;

            for (int x = 0; x < grid.Count; x++)
            {
                for (int y = 0; y < grid.Count; y++)
                {
                    if (IsPresent(x, y, 1, 1, 1) == 1 && (IsPresent(x + 2, y, 1, -1, 1) == 1 || IsPresent(x, y + 2, 1, 1, -1) == 1))
                    {
                        count++;
                    }

                    if (IsPresent(x, y, 1, -1, -1) == 1 && (IsPresent(x, y - 2, 1, -1, 1) == 1 || IsPresent(x - 2, y, 1, 1, -1) == 1))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private int IsPresent(int x, int y, int index, int xdir, int ydir)
        {
            // Base condition
            if (index >= word.Length) return 1;

            // Bounds check
            if (x < 0 || x >= grid.Count || y < 0 || y >= grid[0].Count) return 0;

            if (grid[x][y] == word[index])
            {
                return IsPresent(x + xdir, y + ydir, index + 1, xdir, ydir);
            }

            return 0;
        }
    }
}