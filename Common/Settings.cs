using System.Text.Json;

namespace AOC
{
    public static class Settings
    {
        private static string sessionCookie = string.Empty;
        private static int day = -1;
        private static int year = -1;

        static Settings()
        {
            if (File.Exists("settings.json"))
            {
                string settingsJson = File.ReadAllText("settings.json");
                JsonElement settings = JsonSerializer.Deserialize<JsonElement>(settingsJson);

                sessionCookie = settings.GetProperty("SessionCookie").GetString() ?? string.Empty;
                settings.GetProperty("CurrentDay").TryGetInt32(out day);
                settings.GetProperty("CurrentYear").TryGetInt32(out year);
            }
        }

        public static string SessionCookie { get => sessionCookie; }
        public static int Day { get => day; }
        public static int Year { get => year; }
    }
}