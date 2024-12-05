using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace AOC
{
    public static class InputHelper
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

            var baseAddress = new Uri("https://adventofcode.com/");
            var cookies = new CookieContainer();

            using var handler = new HttpClientHandler() { CookieContainer = cookies };
            using var client = new HttpClient(handler) { BaseAddress = baseAddress };
            cookies.Add(baseAddress, new Cookie("session", Settings.SessionCookie));

            var inputData = await client.GetStringAsync($"{year}/day/{day}/input");

            var result = inputData.Split('\n').ToList();
            while (string.IsNullOrEmpty(result.Last()))
            {
                result.RemoveAt(result.Count - 1);
            }

            if (result.Count() > 0)
            {
                Directory.CreateDirectory(exampleDirectory);
                await File.WriteAllLinesAsync(filename, result);
            }

            return result.ToArray();
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