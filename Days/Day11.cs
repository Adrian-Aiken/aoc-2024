namespace AOC.Y2024
{
    public class Day11 : IProblem
    {
        List<long> baseStones = new List<long>();
        Dictionary<(long, uint), long> stoneCache = new Dictionary<(long, uint), long>();

        public void Parse(string[] input)
        {
            baseStones = input[0].Split(' ').Select(s => long.Parse(s)).ToList();
        }

        public long PartOne()
        {
            return GetCount(25);
        }

        public long PartTwo()
        {
            return GetCount(75);
        }

        public long GetCount(uint iterations)
        {
            long sum = 0;
            foreach (var stone in baseStones)
            {
                sum += ExplodeStones(stone, iterations);
            }

            return sum;
        }

        private void PrintStones(List<long> stones)
        {
            foreach (long stone in stones)
            {
                Console.Write(stone.ToString() + " ");
            }
            Console.WriteLine();
        }

        // Unused; very inefficient
        private List<long> IterateStones(List<long> stones)
        {
            var blink = new List<long>();

            foreach (var stone in stones)
            {
                if (stone == 0)
                {
                    blink.Add(1);
                }
                else if (stone.ToString().Length % 2 == 0)
                {
                    var stoneString = stone.ToString();
                    blink.Add(long.Parse(stoneString.Substring(0, stoneString.Length / 2)));
                    blink.Add(long.Parse(stoneString.Substring(stoneString.Length / 2)));
                }
                else
                {
                    blink.Add(stone * 2024);
                }
            }

            return blink;
        }

        private long ExplodeStones(long value, uint iterations)
        {
            if (stoneCache.ContainsKey((value, iterations)))
            {
                return stoneCache[(value, iterations)];
            }

            if (iterations == 0) return 1;

            long result;
            var stringValue = value.ToString();

            if (value == 0)
            {
                result = ExplodeStones(1, iterations - 1);
            }
            else if (stringValue.Length % 2 == 0)
            {
                var left = long.Parse(stringValue.Substring(0, stringValue.Length / 2));
                var right = long.Parse(stringValue.Substring(stringValue.Length / 2));
                result = ExplodeStones(left, iterations - 1) + ExplodeStones(right, iterations - 1);
            }
            else
            {
                result = ExplodeStones(value * 2024, iterations - 1);
            }

            stoneCache[(value, iterations)] = result;
            return result;
        }
    }
}