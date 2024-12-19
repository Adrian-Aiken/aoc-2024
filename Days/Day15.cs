namespace AOC.Y2024
{
    public class Day15 : IProblem
    {
        private List<List<char>> baseWarehouse = new List<List<char>>();
        private string moves = string.Empty;

        private int x, y;

        public void Parse(string[] input)
        {
            int i = 0;
            while (!string.IsNullOrEmpty(input[i]))
            {
                baseWarehouse.Add(new List<char>(input[i++].ToCharArray()));
            }

            while (i < input.Length)
            {
                moves += input[i++];
            }
        }

        public object PartOne()
        {
            var warehouse = Utils.Duplicate2DList(baseWarehouse);

            var (x, y) = Utils.Find2D(warehouse, '@');

            foreach (var move in moves)
            {
                switch (move)
                {
                    case '<': if (CanMove(warehouse, x, y, 0, -1)) { DoMove(warehouse, x, y, 0, -1); y -= 1; } break;
                    case '>': if (CanMove(warehouse, x, y, 0, 1)) { DoMove(warehouse, x, y, 0, 1); y += 1; } break;
                    case '^': if (CanMove(warehouse, x, y, -1, 0)) { DoMove(warehouse, x, y, -1, 0); x -= 1; } break;
                    case 'v': if (CanMove(warehouse, x, y, 1, 0)) { DoMove(warehouse, x, y, 1, 0); x += 1; } break;
                }
            }

            return GetGpsSum(warehouse, 'O');
        }

        public object PartTwo()
        {
            var warehouse = new List<List<char>>();
            foreach (var baseRow in baseWarehouse)
            {
                var row = new List<char>();
                foreach (var c in baseRow)
                {
                    switch (c)
                    {
                        case '#': row.Add('#'); row.Add('#'); break;
                        case 'O': row.Add('['); row.Add(']'); break;
                        case '.': row.Add('.'); row.Add('.'); break;
                        case '@': row.Add('@'); row.Add('.'); break;
                    }
                }
                warehouse.Add(row);
            }

            var (x, y) = Utils.Find2D(warehouse, '@');
            foreach (var move in moves)
            {
                switch (move)
                {
                    case '<': if (CanMove(warehouse, x, y, 0, -1)) { DoMove(warehouse, x, y, 0, -1); y -= 1; } break;
                    case '>': if (CanMove(warehouse, x, y, 0, 1)) { DoMove(warehouse, x, y, 0, 1); y += 1; } break;
                    case '^': if (CanMove(warehouse, x, y, -1, 0)) { DoMove(warehouse, x, y, -1, 0); x -= 1; } break;
                    case 'v': if (CanMove(warehouse, x, y, 1, 0)) { DoMove(warehouse, x, y, 1, 0); x += 1; } break;
                }
            }

            return GetGpsSum(warehouse, '[');
        }

        private bool CanMove(List<List<char>> warehouse, int x, int y, int dx, int dy)
        {
            var nx = x + dx;
            var ny = y + dy;

            return warehouse[nx][ny] switch
            {
                '#' => false,
                '.' => true,
                '[' => dx == 0 ? CanMove(warehouse, nx, ny, dx, dy) :
                    CanMove(warehouse, nx, ny, dx, dy) && CanMove(warehouse, nx, ny + 1, dx, dy),
                ']' => dx == 0 ? CanMove(warehouse, nx, ny, dx, dy) :
                    CanMove(warehouse, nx, ny, dx, dy) && CanMove(warehouse, nx, ny - 1, dx, dy),
                _ => CanMove(warehouse, nx, ny, dx, dy)
            };
        }

        private void DoMove(List<List<char>> warehouse, int x, int y, int dx, int dy)
        {
            var nx = x + dx;
            var ny = y + dy;

            if (dx != 0 && warehouse[nx][ny] == '[') DoMove(warehouse, nx, ny + 1, dx, dy);
            if (dx != 0 && warehouse[nx][ny] == ']') DoMove(warehouse, nx, ny - 1, dx, dy);
            if (warehouse[nx][ny] != '.') DoMove(warehouse, nx, ny, dx, dy);
            Utils.Swap2D(warehouse, x, y, nx, ny);
        }

        public long GetGpsSum(List<List<char>> warehouse, char box)
        {
            long sum = 0;
            for (int x = 1; x < warehouse.Count; x++)
            {
                for (int y = 1; y < warehouse[x].Count; y++)
                {
                    if (warehouse[x][y] == box)
                    {
                        sum += (x * 100) + y;
                    }
                }
            }

            return sum;
        }
    }
}