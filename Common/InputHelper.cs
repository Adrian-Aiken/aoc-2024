using System.Xml.XPath;
using Microsoft.VisualBasic;

namespace AOC
{
    public static class Website
    {
        private static readonly string inputDirectory = "input";
        private static readonly string exampleDirectory = "input/example";

        public static async Task<string[]> GetInput(int year, int day)
        {
            string filename = $"{inputDirectory}/{day:D2}.txt";
            if (File.Exists(filename))
            {
                return await File.ReadAllLinesAsync(filename);
            }

            // Need to figure out website stuff;
            // Just relying on file input for now
            //var address = new Uri("https://adventofcode.com");
            //using (var client = new HttpClient())
            //{
            //    var response = await client.GetAsync($"https://adventofcode.com/{year}/day/{day}/input");
            //    var responseContent = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine(responseContent);
            //}

            string[] result = new string[2];
            return result;
        }

        public static async Task<string[]> GetExampleInput(int year, int day)
        {
            string filename = $"{exampleDirectory}/{day:d2}.txt";
            if (File.Exists(filename))
            {
                return await File.ReadAllLinesAsync(filename);
            }

            Console.WriteLine("Input Sample (blank to cancel):");
            var result = new List<string>();
            while (true)
            {
                string? line = Console.ReadLine();
                if (string.IsNullOrEmpty(line)) break;

                result.Add(line);
            }

            if (result.Count > 0)
            {
                Directory.CreateDirectory(exampleDirectory);
                await File.WriteAllLinesAsync(filename, result);
            }

            return result.ToArray();
        }
    }
}