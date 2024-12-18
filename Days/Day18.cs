namespace AOC.Y2024
{
    public class Day18 : IProblem
    {
        private const int GRID_SIZE = 70;
        private const int PART_ONE_COUNT = 1024;

        private List<(int, int)> incomingBytes = new List<(int, int)>();

        public void Parse(string[] input)
        {
            foreach (var line in input)
            {
                var ints = line.Split(',').Select(s => int.Parse(s)).ToArray();
                incomingBytes.Add((ints[1], ints[0]));
            }
        }

        public long PartOne()
        {
            var grid = Utils.BlankSquareGrid(GRID_SIZE + 1, '.');

            for (int i = 0; i < PART_ONE_COUNT; i++)
            {
                var (x, y) = incomingBytes[i];
                grid[x][y] = '#';
            }

            return Utils.GetShortestPath(grid, (0, 0), (GRID_SIZE, GRID_SIZE), c => c != '#').Count - 1;
        }

        public long PartTwo()
        {
            var grid = Utils.BlankSquareGrid(GRID_SIZE + 1, '.');

            foreach (var (x, y) in incomingBytes)
            {
                grid[x][y] = '#';
                if (Utils.GetShortestPath(grid, (0, 0), (GRID_SIZE, GRID_SIZE), c => c != '#').Count == 0)
                {
                    Console.WriteLine($"Byte: {y},{x}");
                    return 0;
                }
            }

            return -1;
        }
    }
}