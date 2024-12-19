using System.Diagnostics;

namespace AOC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // ------- Fetch input for day ------------
            Console.Write("Use Real Input? (y/N): ");
            var response = Console.ReadKey();
            Console.WriteLine();

            string[] input = response.Key == ConsoleKey.Y ? await InputHelper.GetInput(Settings.Year, Settings.Day) : await InputHelper.GetExampleInput(Settings.Year, Settings.Day);

            if (input.Count() == 0)
            {
                Console.WriteLine("No input detected; exiting...");
                Console.ReadKey();
                return;
            }

            var type = Type.GetType($"AOC.Y{Settings.Year:D4}.Day{Settings.Day:D2}") ?? throw new Exception("The Type does not seem to exist");
            var dayProblem = (IProblem)(Activator.CreateInstance(type) ?? throw new Exception("The Day Does not seem to exist"));


            // ----------- Run Problems --------------
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            dayProblem.Parse(input);
            stopwatch.Stop();
            var parseTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            var result1 = dayProblem.PartOne().ToString() ?? "Unknown";
            stopwatch.Stop();
            var part1Time = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            var result2 = dayProblem.PartTwo().ToString() ?? "Unknown";
            stopwatch.Stop();
            var part2Time = stopwatch.ElapsedMilliseconds;

            // ----- Prepare and print results --------
            var resultWidth = new[] { "0", result1, result2 }.Select(s => s.Length).Max();
            var timeWidth = new[] { parseTime, part1Time, part2Time }.Select(t => t.ToString().Length).Max();

            Console.WriteLine();
            Console.WriteLine($"+---------{string.Empty.PadLeft(resultWidth + timeWidth, '-')}--+");
            Console.WriteLine($"| {string.Empty.PadLeft(timeWidth)}Results{string.Empty.PadLeft(resultWidth)}   |");
            Console.WriteLine($"+---+-{string.Empty.PadLeft(resultWidth, '-')}-+-{string.Empty.PadLeft(timeWidth, '-')}---+");
            if (parseTime > 10) Console.WriteLine($"| P | {"".PadLeft(resultWidth)} | {parseTime.ToString().PadLeft(timeWidth)}ms |");
            Console.WriteLine($"| 1 | {result1.PadLeft(resultWidth)} | {part1Time.ToString().PadLeft(timeWidth)}ms |");
            Console.WriteLine($"| 2 | {result2.PadLeft(resultWidth)} | {part2Time.ToString().PadLeft(timeWidth)}ms |");
            Console.WriteLine($"+---------{string.Empty.PadLeft(resultWidth + timeWidth, '-')}--+");

            Console.ReadKey();
        }
    }
}