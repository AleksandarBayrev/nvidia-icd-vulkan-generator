namespace NvidiaICDVulkanGenerator
{
    public static class Helpers
    {
        public static string GetFileNotFoundMessage(string path)
        {
            return $"Could not find {path}";
        }
        public static IWorker GetWorker(string param, IVulkanVersionProvider vulkanVersionProvider)
        {
            var vulkanVersion = vulkanVersionProvider.GetVersion();

            if (vulkanVersion == null)
            {
                throw new VulkanVersionNvidiaNotFoundException();
            }

            if (param == Constants.AvailableCommands.Read)
            {
                return new ReadWorker();
            }

            return new CreateOrUpdateWorker(vulkanVersionProvider);
        }
    }
}