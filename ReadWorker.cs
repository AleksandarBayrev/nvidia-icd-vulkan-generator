
using System.Text.Json;

namespace NvidiaICDVulkanGenerator
{
    public class ReadWorker : IWorker
    {
        public async Task Work()
        {
            var filePath = Path.Combine(Constants.BaseJsonPath, Constants.Filename);

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
                System.Console.WriteLine(json?.ToString() ?? Helpers.GetCouldNotParseMessage(filePath));
            }
            catch (Exception)
            {
                System.Console.WriteLine($"Failed to parse {filePath}, please remove it so the program can recreate it.");
            }
            return;
        }
    }
}