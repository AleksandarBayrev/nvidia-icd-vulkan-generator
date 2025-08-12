namespace NvidiaICDVulkanGenerator
{
    public static class Helpers
    {
        public static string GetFileNotFoundMessage(string path)
        {
            return $"Could not find {path}";
        }
        public static IWorker GetWorker(string param, IVulkanVersionProvider vulkanVersionProvider, IFilePathProvider filePathProvider)
        {
            if (param == Constants.AvailableCommands.Read)
            {
                return new ReadWorker(filePathProvider);
            }

            return new CreateOrUpdateWorker(vulkanVersionProvider, filePathProvider);
        }
    }
}