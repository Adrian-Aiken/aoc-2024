using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace AOC.Y2024
{
    public class Day20 : IProblem
    {
        private struct Cheat : IEquatable<Cheat>
        {
            public int x1; public int y1; public int x2; public int y2;
            public Cheat(int x1, int y1, int x2, int y2) { this.x1 = x1; this.x2 = x2; this.y1 = y1; this.y2 = y2; }
            public bool Equals(Cheat o)
            {
                return (x1 == o.x1 && x2 == o.x2 && y1 == o.y1 && y2 == o.y2)
                    || (x1 == o.x2 && x2 == o.x1 && y1 == o.y2 && y2 == o.y1);
            }
        };

        private struct Warp { public int x; public int y; public int dist; }

        private List<List<char>> baseMaze = new List<List<char>>();

        private Dictionary<(int, int), int> distances = new Dictionary<(int, int), int>();
        private IEnumerable<(int, int)> basePath = new List<(int, int)>();
        private (int, int) end;

        public void Parse(string[] input)
        {
            baseMaze = Utils.Parse2DGrid(input);

            var start = Utils.Find2D(baseMaze, 'S');
            end = Utils.Find2D(baseMaze, 'E');
            baseMaze[start.Item1][start.Item2] = '.';
            baseMaze[end.Item1][end.Item2] = '.';
            basePath = Utils.GetShortestPath(baseMaze, start, end, c => c != '#' && c != 'X');

            int i = 0;
            foreach (var spot in basePath)
            {
                distances[spot] = i++;
            }
        }

        public object PartOne()
        {
            var baseTime = basePath.Count() - 1;
            return GetCheatDistances(2)
                .Select(d => baseTime - d)
                .Where(d => d >= 100)
                .Count();
        }

        public object PartTwo()
        {
            var baseTime = basePath.Count() - 1;
            return GetCheatDistances(20)
                .Select(d => baseTime - d)
                .Where(d => d >= 100)
                .Count();
        }

        private List<List<char>> GetCheatedMaze(List<List<char>> maze, Cheat c)
        {
            var newMaze = Utils.Duplicate2DList(maze);
            newMaze[c.x1][c.y1] = 'o';
            return newMaze;
        }

        private IEnumerable<Cheat> GetCheats(List<List<char>> maze, int x, int y)
        {
            if (maze[x + 1][y] == '#')
            {
                if (Utils.IsInBounds(maze, x + 2, y) && maze[x + 2][y] == '.') yield return new Cheat(x + 1, y, x + 2, y);
                else if (Utils.IsInBounds(maze, x + 1, y + 1) && maze[x + 1][y + 1] == '.') yield return new Cheat(x + 1, y, x + 1, y + 1);
                else if (Utils.IsInBounds(maze, x + 1, y - 1) && maze[x + 1][y - 1] == '.') yield return new Cheat(x + 1, y, x + 1, y - 1);
            }
            if (maze[x - 1][y] == '#')
            {
                if (Utils.IsInBounds(maze, x - 2, y) && maze[x - 2][y] == '.') yield return new Cheat(x - 1, y, x - 2, y);
                else if (Utils.IsInBounds(maze, x - 1, y + 1) && maze[x - 1][y + 1] == '.') yield return new Cheat(x - 1, y, x + 1, y + 1);
                else if (Utils.IsInBounds(maze, x - 1, y - 1) && maze[x - 1][y - 1] == '.') yield return new Cheat(x - 1, y, x + 1, y - 1);
            }
            if (maze[x][y + 1] == '#')
            {
                if (Utils.IsInBounds(maze, x, y + 2) && maze[x][y + 2] == '.') yield return new Cheat(x, y + 1, x, y + 2);
                else if (Utils.IsInBounds(maze, x + 1, y + 1) && maze[x + 1][y + 1] == '.') yield return new Cheat(x, y + 1, x + 1, y + 1);
                else if (Utils.IsInBounds(maze, x - 1, y + 1) && maze[x - 1][y + 1] == '.') yield return new Cheat(x, y + 1, x - 1, y + 1);
            }
            if (maze[x][y - 1] == '#')
            {
                if (Utils.IsInBounds(maze, x, y - 2) && maze[x][y - 2] == '.') yield return new Cheat(x, y - 1, x, y - 2);
                else if (Utils.IsInBounds(maze, x + 1, y + 1) && maze[x + 1][y - 1] == '.') yield return new Cheat(x, y - 1, x + 1, y - 1);
                else if (Utils.IsInBounds(maze, x - 1, y + 1) && maze[x - 1][y - 1] == '.') yield return new Cheat(x, y - 1, x - 1, y - 1);
            }
        }

        private IEnumerable<(Cheat, int)> GetValidCheats(List<List<char>> baseMaze, IEnumerable<(int, int)> path, (int, int) end)
        {
            var maze = Utils.Duplicate2DList(baseMaze);
            int progress = 0;
            int baseLength = path.Count();

            foreach (var (x, y) in path)
            {
                foreach (var cheat in GetCheats(maze, x, y))
                {
                    var cheatedMaze = GetCheatedMaze(maze, cheat);
                    var cheatPath = Utils.GetShortestPath(cheatedMaze, (x, y), end, x => x != '#' && x != 'X');

                    if (cheatPath.Count() + progress < baseLength) yield return (cheat, cheatPath.Count() - 1 + progress);
                }

                maze[x][y] = 'X';
                progress++;
            }

            yield break;
        }

        private IEnumerable<int> GetCheatDistances(int cheatTime)
        {
            int baseDistance = 0;
            int targetDistance = basePath.Count();
            foreach (var (x, y) in basePath.Reverse())
            {
                foreach (var warp in GetWarps(x, y, cheatTime).Where(w => IsWarpValid(w)))
                {
                    var newDistance = baseDistance + warp.dist + distances[(warp.x, warp.y)];
                    if (newDistance < targetDistance) yield return newDistance;
                }
                baseDistance++;
            }

            yield break;
        }

        private IEnumerable<Warp> GetWarps(int x, int y, int maxTime)
        {
            for (int i = 0; i <= maxTime; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    int k = i - j;
                    yield return new Warp { x = x + j, y = y + k, dist = i };
                    yield return new Warp { x = x - k, y = y + j, dist = i };
                    yield return new Warp { x = x - j, y = y - k, dist = i };
                    yield return new Warp { x = x + k, y = y - j, dist = i };
                }
            }

            yield break;
        }

        private bool IsWarpValid(Warp warp)
        {
            return Utils.IsInBounds(baseMaze, warp.x, warp.y) && baseMaze[warp.x][warp.y] == '.';
        }
    }
}