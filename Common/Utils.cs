using System.Collections.Immutable;

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

        public static List<List<T>> Duplicate2DList<T>(List<List<T>> list)
        {
            var newList = new List<List<T>>();
            foreach (var row in list)
            {
                newList.Add(new List<T>(row));
            }

            return newList;
        }

        public static void PadGrid<T>(List<List<T>> space, T padValue)
        {
            foreach (var row in space)
            {
                row.Add(padValue);
                row.Insert(0, padValue);
            }

            var newRow = new List<T>();
            for (int i = 0; i < space[0].Count; i++) newRow.Add(padValue);
            space.Add(newRow);
            space.Insert(0, new List<T>(newRow));
        }

        public static void Print2D<T>(IEnumerable<IEnumerable<T>> grid)
        {
            foreach (var row in grid)
            {
                foreach (var item in row)
                {
                    Console.Write(item);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static (int, int) Find2D<T>(IEnumerable<IEnumerable<T>> grid, T value)
        {
            int x = 0;
            foreach (var row in grid)
            {
                int y = 0;
                foreach (var v in row)
                {
                    if (v.Equals(value)) return (x, y);
                    y++;
                }
                x++;
            }

            return (-1, -1);
        }

        public static void Swap2D<T>(List<List<T>> grid, int x, int y, int nx, int ny)
        {
            T temp = grid[x][y];
            grid[x][y] = grid[nx][ny];
            grid[nx][ny] = temp;
        }

        public static List<List<char>> Parse2DGrid(string[] input)
        {
            return Parse2DGrid(input, (c) => c);
        }

        public static List<List<T>> Parse2DGrid<T>(string[] input, Func<char, T> parseFunc)
        {
            var grid = new List<List<T>>();

            foreach (string line in input)
            {
                var row = new List<T>();
                foreach (char c in line)
                {
                    row.Add(parseFunc(c));
                }
                grid.Add(row);
            }

            return grid;
        }

        public static List<List<T>> BlankSquareGrid<T>(int side, T fillItem)
        {
            return BlankGrid(side, side, fillItem);
        }

        public static List<List<T>> BlankGrid<T>(int height, int width, T fillItem)
        {
            var grid = new List<List<T>>();

            for (int x = 0; x < height; x++)
            {
                var row = new List<T>();
                for (int y = 0; y < width; y++)
                {
                    row.Add(fillItem);
                }
                grid.Add(row);
            }

            return grid;
        }

        public static List<(int, int)> GetShortestPath<T>(List<List<T>> grid, (int, int) start, (int, int) end, Func<T, bool> spotValidator)
        {
            var seen = new HashSet<(int, int)>();
            var queue = new Queue<ImmutableList<(int, int)>>();
            queue.Enqueue(ImmutableList.Create(start));

            while (queue.Any())
            {
                var curPath = queue.Dequeue();
                var (x, y) = curPath.Last();

                if (seen.Contains((x, y))) continue;
                seen.Add((x, y));

                if (!IsInBounds(grid, x, y)) continue;
                if (!spotValidator(grid[x][y])) continue;

                if ((x, y) == end) return curPath.ToList();

                queue.Enqueue(curPath.Add((x + 1, y)));
                queue.Enqueue(curPath.Add((x, y + 1)));
                queue.Enqueue(curPath.Add((x - 1, y)));
                queue.Enqueue(curPath.Add((x, y - 1)));
            }

            return new List<(int, int)>();
        }
    }
}