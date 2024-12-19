namespace AOC.Y2024
{
    public class Day06 : IProblem
    {
        List<List<char>> grid = new List<List<char>>();
        int startX, startY;

        int[] xdir = { -1, 0, 1, 0 };
        int[] ydir = { 0, 1, 0, -1 };

        public void Parse(string[] input)
        {
            foreach (string s in input)
            {
                if (s.Contains('^'))
                {
                    startX = grid.Count();
                    startY = s.IndexOf('^');
                }

                grid.Add(s.ToCharArray().ToList());
            }
        }

        public object PartOne()
        {
            var testGrid = new List<List<char>>();
            foreach (var s in grid)
            {
                testGrid.Add(new List<char>(s));
            }

            int x = startX, y = startY, turn = 0;
            while (true)
            {
                // Mark spot
                testGrid[x][y] = 'X';

                var nextX = x + xdir[turn];
                var nextY = y + ydir[turn];

                if (!Utils.IsInBounds(testGrid, nextX, nextY)) break;

                // Check if turning
                if (testGrid[nextX][nextY] == '#')
                {
                    turn = (turn + 1) % 4;
                }
                else
                {
                    // Move
                    x += xdir[turn];
                    y += ydir[turn];
                }
            }

            return testGrid.SelectMany(s => s).Count(c => c == 'X');
        }

        public object PartTwo()
        {
            int looping = 0;
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid.Count; j++)
                {
                    if (grid[i][j] == '.' && willLoop(i, j))
                    {
                        looping++;
                    }
                }
            }

            return looping;
        }

        private bool willLoop(int obstacleX, int obstacleY)
        {
            var testGrid = new List<List<char>>();
            foreach (var s in grid)
            {
                testGrid.Add(new List<char>(s));
            }

            testGrid[obstacleX][obstacleY] = '#';
            testGrid[startX][startY] = '.';

            int x = startX, y = startY, turn = 0;
            while (true)
            {
                // Check spot & mark
                if (testGrid[x][y] == '4') return true;
                else if (testGrid[x][y] >= '1' && testGrid[x][y] <= '3') testGrid[x][y]++;
                else testGrid[x][y] = '1';

                var nextX = x + xdir[turn];
                var nextY = y + ydir[turn];

                if (!Utils.IsInBounds(testGrid, nextX, nextY)) return false;

                // Check if turning
                if (testGrid[nextX][nextY] == '#')
                {
                    turn = (turn + 1) % 4;
                }
                else
                {
                    // Move
                    x += xdir[turn];
                    y += ydir[turn];
                }
            }
        }
    }
}