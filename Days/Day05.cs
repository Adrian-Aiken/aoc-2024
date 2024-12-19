using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime;

namespace AOC.Y2024
{
    public class Day05 : IProblem
    {
        private Dictionary<int, List<int>> rules = new Dictionary<int, List<int>>();
        private List<List<int>> updates = new List<List<int>>();

        public void Parse(string[] input)
        {
            int i = 0;
            while (!string.IsNullOrEmpty(input[i]))
            {
                var pair = input[i++].Split('|').Select(x => int.Parse(x)).ToArray()
                ;
                if (!rules.ContainsKey(pair[0]))
                    rules[pair[0]] = new List<int>();

                rules[pair[0]].Add(pair[1]);
            }

            i++;

            for (; i < input.Count(); i++)
            {
                updates.Add(input[i].Split(',').Select(x => int.Parse(x)).ToList());
            }
        }

        public object PartOne()
        {
            int sum = 0;
            foreach (var update in updates)
            {
                if (isValid(update))
                {
                    sum += update[update.Count / 2];
                }
            }

            return sum;
        }

        public object PartTwo()
        {
            int sum = 0;
            foreach (var update in updates)
            {
                if (!isValid(update))
                {
                    fixUpdateList(update);
                    sum += update[update.Count / 2];
                }
            }

            return sum;
        }

        private bool isValid(List<int> update)
        {
            for (int i = 0; i < update.Count; i++)
            {
                for (int j = i + 1; j < update.Count; j++)
                {
                    if (!rules.ContainsKey(update[i]) || !rules[update[i]].Contains(update[j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void fixUpdateList(List<int> update)
        {
            for (int i = 0; i < update.Count; i++)
            {
                for (int j = i + 1; j < update.Count; j++)
                {
                    if (!rules.ContainsKey(update[i]) || !rules[update[i]].Contains(update[j]))
                    {
                        (update[j], update[i]) = (update[i], update[j]);
                    }
                }
            }
        }
    }
}