using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;

namespace AOC.Y2024
{
    public class Day22 : IProblem
    {
        private List<uint> initialNumbers = new List<uint>();

        private List<Dictionary<(int, int, int, int), int>> pricePatterns = new List<Dictionary<(int, int, int, int), int>>();

        public void Parse(string[] input)
        {
            initialNumbers.AddRange(input.Select(uint.Parse));
        }

        public object PartOne()
        {
            return initialNumbers.Select(n => (long)GetNthNumber(n, 2000)).Sum();
        }

        public object PartTwo()
        {
            GeneratePriceSignals();

            //var price = GetReturns((-2, 1, -1, 3));

            int maxPrice = 0;
            for (int i = -9; i <= 9; i++)
                for (int j = -9; j <= 9; j++)
                    for (int k = -9; k <= 9; k++)
                        for (int l = -9; l <= 9; l++)
                            maxPrice = Math.Max(maxPrice, GetReturns((i, j, k, l)));


            return maxPrice;
        }

        private uint GenNumber(uint number)
        {
            number = (number ^ (number << 6)) % 16777216;
            number = (number ^ (number >> 5)) % 16777216;
            number = (number ^ (number << 11)) % 16777216;
            return number;
        }

        private uint GetNthNumber(uint initial, int n)
        {
            uint num = initial;
            for (int i = 0; i < n; i++) num = GenNumber(num);
            return num;
        }

        private void GeneratePriceSignals()
        {
            foreach (var num in initialNumbers)
            {
                uint lastNum = num;
                int lastPrice = (int)num % 10;
                var prices = new List<int>();
                var changes = new List<int>();

                prices.Add(lastPrice);
                changes.Add(-50);

                for (int i = 0; i < 2000; i++)
                {
                    uint nextNum = GenNumber(lastNum);
                    int nextPrice = (int)(nextNum % 10);
                    prices.Add((int)nextPrice);
                    changes.Add(nextPrice - lastPrice);
                    lastPrice = nextPrice;
                    lastNum = nextNum;
                }

                GeneratePatterns(changes, prices);
            }
        }

        private void GeneratePatterns(List<int> changes, List<int> prices)
        {
            var patterns = new Dictionary<(int, int, int, int), int>();
            for (int i = 0; i < changes.Count - 3; i++)
            {
                var pattern = (changes[i], changes[i + 1], changes[i + 2], changes[i + 3]);
                if (!patterns.ContainsKey(pattern)) patterns[pattern] = prices[i + 3];
            }
            pricePatterns.Add(patterns);
        }

        private int GetReturns((int, int, int, int) pattern)
        {
            if (!PatternValid(pattern)) return 0;

            int sum = 0;
            foreach (var monkey in pricePatterns)
            {
                if (monkey.TryGetValue(pattern, out int price))
                {
                    sum += price;
                }
            }

            return sum;
        }

        private bool PatternValid((int, int, int, int) pattern)
        {
            var change = pattern.Item1;
            change += pattern.Item2;
            if (Math.Abs(change) > 9) return false;
            change += pattern.Item3;
            if (Math.Abs(change) > 9) return false;
            change += pattern.Item4;
            if (Math.Abs(change) > 9) return false;
            return true;
        }
    }
}