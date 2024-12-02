using AOC.Y2024;

namespace AOC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Manually doing it for now
            // Will probably do a smarter one later this week

            // Fetch input for day
            Console.Write("Use Real Input? (y/N): ");
            var response = Console.ReadKey();
            Console.WriteLine();

            string[] input = response.Key == ConsoleKey.Y ? await InputHelper.GetInput(2024, 2) : await InputHelper.GetExampleInput(2024, 2);

            if (input.Count() == 0)
            {
                Console.WriteLine("No input detected; exiting...");
                Console.ReadKey();
                return;
            }

            var dayone = new Day02();
            dayone.Parse(input);

            var result1 = dayone.PartOne();
            Console.WriteLine($"Part 1: {result1}");

            var result2 = dayone.PartTwo();
            Console.WriteLine($"Part 2: {result2}");

            Console.ReadKey();
        }
    }
}