namespace AOC
{
    public static class Website
    {
        public static async Task<string[]> GetInput(int year, int day)
        {
            string filename = $"input/{day:D2}.txt";
            if (File.Exists(filename))
            {
                return File.ReadAllLines(filename);
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
    }
}