namespace NvidiaICDVulkanGenerator
{
    public class FilePathProvider : IFilePathProvider
    {
        public string GetFilePath()
        {
            return Path.Combine(Constants.BaseJsonPath, Constants.Filename);
        }
    }
}