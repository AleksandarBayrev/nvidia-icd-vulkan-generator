namespace NvidiaICDVulkanGenerator
{
    public static class Helpers
    {
        public static string GetFileNotFoundMessage(string path)
        {
            return $"Could not find {path}";
        }
        public static string GetCouldNotParseMessage(string path)
        {
            return $"Could not parse {path}";
        }
    }
}