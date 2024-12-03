using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace AOC.Y2024
{
    public class Day03 : IProblem
    {
        private readonly List<int> left = new List<int>();
        private readonly List<int> right = new List<int>();

        private readonly string mulPattern = @"(?:mul\(\d+\,\d+\))";
        private readonly string mulSwitchPattern = @"(?:mul\(\d+\,\d+\))|(?:do\(\))|(?:don't\(\))";

        private string memory = string.Empty;

        public void Parse(string[] input)
        {
            memory = string.Join(string.Empty, input);
        }

        public int PartOne()
        {
            var matches = Regex.Matches(memory, mulPattern);

            int sum = 0;
            foreach (var match in matches)
            {
                var matchString = match.ToString() ?? string.Empty;
                sum += ParseMulString(matchString);
            }

            return sum;
        }

        public int PartTwo()
        {
            var matches = Regex.Matches(memory, mulSwitchPattern);

            int sum = 0;
            bool enabled = true;
            foreach (var match in matches)
            {
                var matchString = match.ToString() ?? string.Empty;
                switch (matchString)
                {
                    case "do()": enabled = true; break;
                    case "don't()": enabled = false; break;
                    default: if (enabled) { sum += ParseMulString(matchString); } break;
                }
            }

            return sum;
        }

        private int ParseMulString(string s)
        {
            var pair = s.ToString().Substring("mul(".Length).Replace(")", "").Split(',');

            return int.Parse(pair[0]) * int.Parse(pair[1]);
        }
    }
}