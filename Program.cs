using AOC.Y2024;

namespace AOC
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            // Manually doing it for now
            // Will probably do a smarter one later this week

            // Fetch input for day
            var input = await Website.GetInput(2023, 1);

            var dayone = new Day01();
            dayone.Parse(input);

            var result1 = dayone.PartOne();
        }
    }
}