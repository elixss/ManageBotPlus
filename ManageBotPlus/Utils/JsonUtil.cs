using System.Text.Json;

namespace ManageBotPlus
{
    internal static class JsonUtil
    {
        public static async Task<T> GetJsonAsync<T>(string fileName)
        {
            using Stream? stream = typeof(JsonUtil).Assembly.GetManifestResourceStream($"ManageBotPlus.Json.{fileName}");
            using StreamReader reader = new(stream);
            T? returnValue = JsonSerializer.Deserialize<T>(await reader.ReadToEndAsync());
            return returnValue;
        }

        public static async Task<T> GetJsonAsync<T>(Stream stream)
        {
            using StreamReader reader = new(stream);
            T? returnValue = JsonSerializer.Deserialize<T>(await reader.ReadToEndAsync());
            return returnValue;
        }
    }
}
