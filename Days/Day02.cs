namespace AOC.Y2024
{
    public class Day02 : IProblem
    {
        List<List<int>> reports = new List<List<int>>();

        public void Parse(string[] input)
        {
            foreach (string s in input)
            {
                reports.Add(new List<int>(s.Split(" ").Select(x => int.Parse(x))));
            }
        }

        public object PartOne()
        {
            int safe = 0;
            foreach (var report in reports)
            {
                if (isSafe(report))
                {
                    safe++;
                }
            }

            return safe;
        }

        public object PartTwo()
        {
            int safe = 0;
            foreach (var report in reports)
            {
                if (isSafe(report))
                {
                    safe++;
                    continue;
                }

                for (int i = 0; i < report.Count; i++)
                {
                    var reportCopy = new List<int>(report);
                    reportCopy.RemoveAt(i);

                    if (isSafe(reportCopy))
                    {
                        safe++;
                        break;
                    }
                }
            }

            return safe;
        }

        private bool isSafe(List<int> report)
        {
            return (isDecreasing(report) || isIncreasing(report)) && isGentle(report);
        }

        private bool isDecreasing(List<int> report)
        {
            for (int i = 0; i < report.Count - 1; i++)
            {
                if (report[i + 1] >= report[i])
                {
                    return false;
                }
            }

            return true;
        }

        private bool isIncreasing(List<int> report)
        {
            for (int i = 0; i < report.Count - 1; i++)
            {
                if (report[i + 1] <= report[i])
                {
                    return false;
                }
            }

            return true;
        }

        private bool isGentle(List<int> report)
        {
            for (int i = 0; i < report.Count - 1; i++)
            {
                int diff = Math.Abs(report[i + 1] - report[i]);
                if (diff < 1 || diff > 3) return false;
            }

            return true;
        }
    }
}