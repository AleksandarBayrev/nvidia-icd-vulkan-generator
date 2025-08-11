using System.Diagnostics;

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
        public static IWorker GetWorker(string param, IVulkanVersionProvider vulkanVersionProvider)
        {
            if (param == Constants.AvailableCommands.Read)
            {
                return new ReadWorker();
            }
            return new CreateOrUpdateWorker(vulkanVersionProvider);
        }
    }
}