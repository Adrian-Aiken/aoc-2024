using System.Data;

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
    }
}