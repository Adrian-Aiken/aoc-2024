namespace AOC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Fetch input for day
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

            var result1 = dayProblem.PartOne();
            Console.WriteLine($"Part 1: {result1}");

            var result2 = dayProblem.PartTwo();
            Console.WriteLine($"Part 2: {result2}");

            Console.ReadKey();
        }
    }
}