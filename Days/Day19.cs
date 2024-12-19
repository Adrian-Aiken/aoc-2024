using System.Collections.Immutable;
using System.Net.Http.Headers;

namespace AOC.Y2024
{
    public class Day19 : IProblem
    {
        private List<string> towels = new List<string>();
        private List<string> patterns = new List<string>();

        public Dictionary<string, ImmutableList<ImmutableList<string>>> knownPatterns = new Dictionary<string, ImmutableList<ImmutableList<string>>>();
        public Dictionary<string, long> knownPatternCounts = new Dictionary<string, long>();

        public void Parse(string[] input)
        {
            towels.AddRange(input[0].Split(", "));
            patterns.AddRange(input.Skip(2));
        }

        public object PartOne()
        {
            return patterns.Where(p => IsAnyPatternPossible(p)).Count();
        }

        public object PartTwo()
        {
            return patterns.Where(p => IsAnyPatternPossible(p)).Sum(p => GetArrangementCount(p));
        }

        private bool IsAnyPatternPossible(string pattern)
        {
            if (pattern.Length == 0) return true;

            foreach (var towel in towels)
            {
                if (pattern.StartsWith(towel) && IsAnyPatternPossible(pattern.Substring(towel.Length)))
                {
                    return true;
                }
            }

            return false;
        }

        private long GetArrangementCount(string pattern)
        {
            if (knownPatternCounts.TryGetValue(pattern, out var cachedResult)) return cachedResult;

            long sum = 0;
            foreach (var towel in towels)
            {
                if (pattern == towel) sum++;
                else if (pattern.StartsWith(towel)) sum += GetArrangementCount(pattern[towel.Length..]);
            }

            knownPatternCounts[pattern] = sum;
            return sum;
        }
    }
}