namespace AOC.Y2024
{
    public class Day10 : IProblem
    {
        List<List<int>> map = new List<List<int>>();

        public void Parse(string[] input)
        {
            foreach (string s in input)
            {
                map.Add(new List<int>(s.Select(c => c - '0')));
            }
        }

        public object PartOne()
        {
            int sum = 0;
            for (int x = 0; x < map.Count; x++)
            {
                for (int y = 0; y < map[0].Count; y++)
                {
                    sum += CountTrails(x, y);
                }
            }

            return sum;
        }

        public object PartTwo()
        {
            int sum = 0;
            for (int x = 0; x < map.Count; x++)
            {
                for (int y = 0; y < map[0].Count; y++)
                {
                    sum += CountUniqueTrails(x, y);
                }
            }

            return sum;
        }

        private int CountTrails(int x, int y)
        {
            var trailEnds = new HashSet<(int, int)>();
            GetTrailEnds(x, y, 0, trailEnds);
            return trailEnds.Count;
        }

        private void GetTrailEnds(int x, int y, int height, HashSet<(int, int)> knownEnds)
        {
            if (!Utils.IsInBounds(map, x, y) || map[x][y] != height) return;

            if (height == 9 && map[x][y] == height)
            {
                knownEnds.Add((x, y));
                return;
            }

            GetTrailEnds(x + 1, y, height + 1, knownEnds);
            GetTrailEnds(x - 1, y, height + 1, knownEnds);
            GetTrailEnds(x, y + 1, height + 1, knownEnds);
            GetTrailEnds(x, y - 1, height + 1, knownEnds);
        }

        private int CountUniqueTrails(int x, int y, int height = 0)
        {
            if (height == 9 && Utils.IsInBounds(map, x, y) && map[x][y] == height) return 1;
            if (!Utils.IsInBounds(map, x, y) || map[x][y] != height) return 0;

            return CountUniqueTrails(x + 1, y, height + 1)
                 + CountUniqueTrails(x - 1, y, height + 1)
                 + CountUniqueTrails(x, y + 1, height + 1)
                 + CountUniqueTrails(x, y - 1, height + 1);
        }
    }
}