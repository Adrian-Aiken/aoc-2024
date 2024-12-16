using System.Collections.Immutable;

namespace AOC.Y2024
{
    public class Day16 : IProblem
    {
        private List<List<char>> maze = new List<List<char>>();
        private long bestScore = long.MaxValue;
        private HashSet<(int, int)> bestPaths = new HashSet<(int, int)>();

        public void Parse(string[] input)
        {
            maze = Utils.Parse2DGrid(input, (char c) => c);
        }

        public long PartOne()
        {
            bestScore = GetLowestScore(maze);
            return bestScore;
        }

        public long PartTwo()
        {
            GetMazePaths(maze);
            return bestPaths.Count;
        }

        private long GetLowestScore(List<List<char>> maze)
        {
            var seen = new HashSet<(int, int, char)>();
            var queue = new PriorityQueue<(int, int, char), long>();

            var (startX, startY) = Utils.Find2D(maze, 'S');

            var startVal = (startX, startY, '>');
            queue.Enqueue(startVal, 0);

            while (queue.TryDequeue(out var currentSpot, out var score))
            {
                if (seen.Contains(currentSpot)) continue;

                var (x, y, dir) = currentSpot;
                if (maze[x][y] == 'E') return score;
                if (maze[x][y] == '#') continue;
                seen.Add(currentSpot);

                long upTurnScore = 0, downTurnScore = 0, leftTurnScore = 0, rightTurnScore = 0;
                switch (dir)
                {
                    case '^': upTurnScore = 0000; leftTurnScore = 1000; downTurnScore = 2000; rightTurnScore = 1000; break;
                    case 'V': upTurnScore = 2000; leftTurnScore = 1000; downTurnScore = 0000; rightTurnScore = 1000; break;
                    case '<': upTurnScore = 1000; leftTurnScore = 0000; downTurnScore = 1000; rightTurnScore = 2000; break;
                    case '>': upTurnScore = 1000; leftTurnScore = 2000; downTurnScore = 1000; rightTurnScore = 0000; break;
                }

                // Enqueue each turn
                queue.Enqueue((x - 1, y, '^'), score + 1 + upTurnScore);
                queue.Enqueue((x + 1, y, 'V'), score + 1 + downTurnScore);
                queue.Enqueue((x, y - 1, '<'), score + 1 + leftTurnScore);
                queue.Enqueue((x, y + 1, '>'), score + 1 + rightTurnScore);
            }

            return -1;
        }

        private void GetMazePaths(List<List<char>> maze)
        {
            var stack = new Stack<(int, int, char, ImmutableList<(int, int)>, long)>();
            var (startX, startY) = Utils.Find2D(maze, 'S');
            var seen = new Dictionary<(int, int, char), long>();
            stack.Push((startX, startY, '>', ImmutableList.Create<(int, int)>(), 0));

            while (stack.Count > 0)
            {
                var (x, y, dir, path, score) = stack.Pop();
                if (maze[x][y] == '#') continue;
                if (score > bestScore) continue;
                if (maze[x][y] == 'E')
                {
                    foreach (var tile in path) bestPaths.Add(tile);
                    bestPaths.Add((x, y));
                    continue;
                }

                if (seen.TryGetValue((x, y, dir), out var seenScore))
                {
                    if (score > seenScore) continue;
                }
                seen[(x, y, dir)] = score;

                long upTurnScore = 0, downTurnScore = 0, leftTurnScore = 0, rightTurnScore = 0;
                switch (dir)
                {
                    case '^': upTurnScore = 0000; leftTurnScore = 1000; downTurnScore = 2000; rightTurnScore = 1000; break;
                    case 'V': upTurnScore = 2000; leftTurnScore = 1000; downTurnScore = 0000; rightTurnScore = 1000; break;
                    case '<': upTurnScore = 1000; leftTurnScore = 0000; downTurnScore = 1000; rightTurnScore = 2000; break;
                    case '>': upTurnScore = 1000; leftTurnScore = 2000; downTurnScore = 1000; rightTurnScore = 0000; break;
                }

                // Enqueue each turn
                var newPath = path.Add((x, y));
                stack.Push((x - 1, y, '^', newPath, score + 1 + upTurnScore));
                stack.Push((x + 1, y, 'V', newPath, score + 1 + downTurnScore));
                stack.Push((x, y - 1, '<', newPath, score + 1 + leftTurnScore));
                stack.Push((x, y + 1, '>', newPath, score + 1 + rightTurnScore));
            }
        }
    }
}