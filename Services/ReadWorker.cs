using System.Text.Json;

namespace NvidiaICDVulkanGenerator
{
    public class ReadWorker : IWorker
    {
        private IFilePathProvider _filePathProvider;

        public ReadWorker(IFilePathProvider filePathProvider)
        {
            _filePathProvider = filePathProvider;
        }

        public async Task Work()
        {
            var filePath = _filePathProvider.GetFilePath();

            System.Console.WriteLine($"Executing read on {filePath}");

            var hasFile = File.Exists(filePath);
            if (!hasFile)
            {
                System.Console.WriteLine(Helpers.GetFileNotFoundMessage(filePath));
                return;
            }

            try
            {
                var json = JsonSerializer.Deserialize<JsonModel>(await File.ReadAllTextAsync(filePath), Options.JsonSerializerOptions);
                System.Console.WriteLine(json?.ToString() ?? GetCouldNotParseMessage(filePath));
            }
            catch (Exception)
            {
                System.Console.WriteLine($"Failed to parse {filePath}, please remove it so the program can recreate it.");
            }
            return;
        }
        private static string GetCouldNotParseMessage(string path)
        {
            return $"Could not parse {path}";
        }
    }
}