namespace AOC.Y2024
{
    public class Day01 : IProblem
    {
        private readonly List<int> left = new List<int>();
        private readonly List<int> right = new List<int>();

        public void Parse(string[] input)
        {
            foreach (string s in input)
            {
                var pair = s.Split("   ");
                left.Add(int.Parse(pair[0]));
                right.Add(int.Parse(pair[1]));
            }
        }

        public int PartOne()
        {
            var leftSorted = new List<int>(left);
            var rightSorted = new List<int>(right);
            leftSorted.Sort();
            rightSorted.Sort();

            int sum = 0;

            for (int i = 0; i < leftSorted.Count; i++)
            {
                sum += Math.Abs(leftSorted[i] - rightSorted[i]);
            }

            return sum;
        }

        public int PartTwo()
        {
            var freqDict = right.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

            int sum = 0;
            foreach (int val in left)
            {
                if (freqDict.TryGetValue(val, out int freq))
                {
                    sum += val * freq;
                }
            }

            return sum;
        }
    }
}