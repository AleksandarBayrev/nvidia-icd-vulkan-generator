using System.Text.Json;

namespace NvidiaICDVulkanGenerator
{
    public static class Options
    {
        static Options()
        {
            _jsonSerializerOptions = new JsonSerializerOptions();
            _jsonSerializerOptions.WriteIndented = true;
        }
        private static JsonSerializerOptions _jsonSerializerOptions;

        public static JsonSerializerOptions JsonSerializerOptions => _jsonSerializerOptions;
    }
}