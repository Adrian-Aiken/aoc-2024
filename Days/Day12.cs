namespace AOC.Y2024
{
    public class Day12 : IProblem
    {
        List<List<char>> baseGarden = new List<List<char>>();
        List<(int, int, int)> plotData = new List<(int, int, int)>();

        public void Parse(string[] input)
        {
            foreach (var s in input)
            {
                baseGarden.Add(s.ToList());
            }

            Utils.PadGrid(baseGarden, '.');
            for (int i = 0; i < baseGarden.Count; i++)
            {
                for (int j = 0; j < baseGarden[i].Count; j++)
                {
                    plotData.Add(GetFenceInfo(baseGarden, i, j));
                }
            }
        }

        public object PartOne()
        {
            long sum = 0;
            foreach (var plot in plotData)
            {
                var (area, perimeter, corners) = plot;
                sum += area * perimeter;
            }

            return sum;
        }

        public object PartTwo()
        {
            long sum = 0;
            foreach (var plot in plotData)
            {
                var (area, perimeter, corners) = plot;
                sum += area * corners;
            }

            return sum;
        }

        private (int, int, int) GetFenceInfo(List<List<char>> garden, int row, int col)
        {
            char crop = garden[row][col];
            if (crop == '.') return (0, 0, 0);

            int area = 0, perimeter = 0, corners = 0;

            var toCheck = new Queue<(int, int)>();
            var visited = new HashSet<(int, int)>();
            toCheck.Enqueue((row, col));

            while (toCheck.Any())
            {
                var (x, y) = toCheck.Dequeue();
                if (garden[x][y] != crop || visited.Contains((x, y))) continue;

                visited.Add((x, y));
                area++;

                // Peremeters
                if (garden[x - 1][y] != crop) perimeter++;
                if (garden[x + 1][y] != crop) perimeter++;
                if (garden[x][y + 1] != crop) perimeter++;
                if (garden[x][y - 1] != crop) perimeter++;

                // Outer corners
                if (garden[x - 1][y] != crop && garden[x][y - 1] != crop) corners++;
                if (garden[x - 1][y] != crop && garden[x][y + 1] != crop) corners++;
                if (garden[x + 1][y] != crop && garden[x][y - 1] != crop) corners++;
                if (garden[x + 1][y] != crop && garden[x][y + 1] != crop) corners++;

                // Inner corners
                if (garden[x - 1][y] == crop && garden[x][y - 1] == crop && garden[x - 1][y - 1] != crop) corners++;
                if (garden[x - 1][y] == crop && garden[x][y + 1] == crop && garden[x - 1][y + 1] != crop) corners++;
                if (garden[x + 1][y] == crop && garden[x][y - 1] == crop && garden[x + 1][y - 1] != crop) corners++;
                if (garden[x + 1][y] == crop && garden[x][y + 1] == crop && garden[x + 1][y + 1] != crop) corners++;

                // Enqueue neighbors
                toCheck.Enqueue((x + 1, y));
                toCheck.Enqueue((x - 1, y));
                toCheck.Enqueue((x, y + 1));
                toCheck.Enqueue((x, y - 1));
            }

            // Clear area
            toCheck.Enqueue((row, col));

            while (toCheck.Any())
            {
                var (x, y) = toCheck.Dequeue();
                if (garden[x][y] != crop) continue;

                garden[x][y] = '.';

                if (garden[x - 1][y] == crop) toCheck.Enqueue((x - 1, y));
                if (garden[x + 1][y] == crop) toCheck.Enqueue((x + 1, y));
                if (garden[x][y + 1] == crop) toCheck.Enqueue((x, y + 1));
                if (garden[x][y - 1] == crop) toCheck.Enqueue((x, y - 1));
            }

            return (area, perimeter, corners);
        }
    }
}