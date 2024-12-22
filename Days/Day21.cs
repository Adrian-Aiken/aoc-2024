using System.Text;

namespace AOC.Y2024
{
    public class Day21 : IProblem
    {
        private List<string> patterns = new List<string>();
        private Dictionary<(char, char), string> directionalPatterns = new Dictionary<(char, char), string>()
        {
            {('A', 'A'), ""}, {('A', '^'), "<"}, {('A', '>'), "v"}, {('A', 'v'), "v<"}, {('A', '<'), "v<<"},
            {('^', 'A'), ">"}, {('^', '^'), ""}, {('^', '>'), "v>"}, {('^', 'v'), "v"}, {('^', '<'), "v<"},
            {('<', 'A'), "^>>"}, {('<', '^'), "^>"}, {('<', '>'), ">>"}, {('<', 'v'), ">"}, {('<', '<'), ""},
            {('v', 'A'), "^>"}, {('v', '^'), "^"}, {('v', '>'), ">"}, {('v', 'v'), ""}, {('v', '<'), "<"},
            {('>', 'A'), "^"}, {('>', '^'), "^<"}, {('>', '>'), ""}, {('>', 'v'), "<"}, {('>', '<'), "<<"},
        };
        private Dictionary<(char, char, int), long> patternDepthLength = new Dictionary<(char, char, int), long>();

        public void Parse(string[] input)
        {
            patterns.AddRange(input);
        }

        public object PartOne()
        {
            return patterns.Sum(p => GetScore(p, GetPatternLength(p, 2)));
        }

        public object PartTwo()
        {
            return patterns.Sum(p => GetScore(p, GetPatternLength(p, 25)));
        }

        private long GetPatternLength(string pattern, int iterations)
        {
            long sum = 0;
            char curChar = 'A';
            foreach (char c in pattern)
            {
                sum += GetNumericLength(curChar, c, iterations);
                curChar = c;
            }

            return sum;
        }

        private long GetNumericLength(char start, char end, int iterations)
        {
            var x = start switch { '7' or '8' or '9' => 0, '4' or '5' or '6' => 1, '1' or '2' or '3' => 2, '0' or 'A' => 3, _ => -1 };
            var y = start switch { '7' or '4' or '1' => 0, '8' or '5' or '2' or '0' => 1, '9' or '6' or '3' or 'A' => 2, _ => -1 };
            var dx = end switch { '7' or '8' or '9' => 0, '4' or '5' or '6' => 1, '1' or '2' or '3' => 2, '0' or 'A' => 3, _ => -1 };
            var dy = end switch { '7' or '4' or '1' => 0, '8' or '5' or '2' or '0' => 1, '9' or '6' or '3' or 'A' => 2, _ => -1 };

            var nextPattern = new StringBuilder();
            while (y > dy) { nextPattern.Append('<'); y--; }
            while (y < dy) { nextPattern.Append('>'); y++; }
            while (x > dx) { nextPattern.Append('^'); x--; }
            while (x < dx) { nextPattern.Append('v'); x++; }

            return nextPattern.ToString()
                .ToCharArray()
                .Permute()
                .Select(c => string.Join("", c) + 'A')
                .Distinct()
                .Where(p => IsPatternValid(p, start))
                .Select(p => GetPatternDirectionalLength(p, iterations))
                .Min();
        }

        private long GetPatternDirectionalLength(string pattern, int iterations)
        {
            long sum = 0;
            char curChar = 'A';
            foreach (char c in pattern)
            {
                sum += GetDirectionalLength(curChar, c, iterations);
                curChar = c;
            }

            return sum;
        }

        private long GetDirectionalLength(char start, char end, int iterations)
        {
            var nextPattern = directionalPatterns[(start, end)];
            if (iterations == 1) return nextPattern.Length + 1;
            if (patternDepthLength.TryGetValue((start, end, iterations), out var cached)) return cached;

            var length = nextPattern.Permute()
                .Select(c => string.Join("", c) + 'A')
                .Distinct()
                .Where(p => IsDirectionalValid(p, start))
                .Select(p => GetPatternDirectionalLength(p, iterations - 1))
                .Min();

            patternDepthLength[(start, end, iterations)] = length;
            return length;
        }

        private bool IsPatternValid(string pattern, char start)
        {
            var x = start switch { '7' or '8' or '9' => 0, '4' or '5' or '6' => 1, '1' or '2' or '3' => 2, '0' or 'A' => 3, _ => -1 };
            var y = start switch { '7' or '4' or '1' => 0, '8' or '5' or '2' or '0' => 1, '9' or '6' or '3' or 'A' => 2, _ => -1 };

            foreach (char c in pattern)
            {
                switch (c)
                {
                    case '^': x -= 1; break;
                    case 'v': x += 1; break;
                    case '<': y -= 1; break;
                    case '>': y += 1; break;
                }

                if (x == 3 && y == 0) return false;
            }

            return true;
        }

        private bool IsDirectionalValid(string pattern, char start)
        {
            var x = start switch { '^' or 'A' => 0, _ => 1 };
            var y = start switch { '<' => 0, '^' or 'v' => 1, _ => 2 };

            foreach (char c in pattern)
            {
                switch (c)
                {
                    case '^': x -= 1; break;
                    case 'v': x += 1; break;
                    case '<': y -= 1; break;
                    case '>': y += 1; break;
                }

                if (x == 0 && y == 0) return false;
            }

            return true;
        }

        private long GetScore(string code, long length)
        {
            long codeScore = long.Parse(new string(code.Where(char.IsDigit).ToArray()));
            return codeScore * length;
        }
    }
}