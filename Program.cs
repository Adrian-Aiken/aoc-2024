﻿using System.Diagnostics;

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

            dayProblem.Parse(input);

            // ----------- Run Problems --------------
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var result1 = dayProblem.PartOne();
            stopwatch.Stop();
            var part1Time = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            var result2 = dayProblem.PartTwo();
            stopwatch.Stop();
            var part2Time = stopwatch.ElapsedMilliseconds;

            // ----- Prepare and print results --------
            var resultWidth = Math.Max(result1.ToString().Length, result2.ToString().Length);
            var timeWidth = Math.Max(part1Time.ToString().Length, part2Time.ToString().Length);

            Console.WriteLine();
            Console.WriteLine($"+---------{string.Empty.PadLeft(resultWidth + timeWidth, '-')}--+");
            Console.WriteLine($"| {string.Empty.PadLeft(timeWidth)}Results{string.Empty.PadLeft(resultWidth)}   |");
            Console.WriteLine($"+---+-{string.Empty.PadLeft(resultWidth, '-')}-+-{string.Empty.PadLeft(timeWidth, '-')}---+");
            Console.WriteLine($"| 1 | {result1.ToString().PadLeft(resultWidth)} | {part1Time.ToString().PadLeft(timeWidth)}ms |");
            Console.WriteLine($"| 2 | {result2.ToString().PadLeft(resultWidth)} | {part2Time.ToString().PadLeft(timeWidth)}ms |");
            Console.WriteLine($"+---------{string.Empty.PadLeft(resultWidth + timeWidth, '-')}--+");

            Console.ReadKey();
        }
    }
}