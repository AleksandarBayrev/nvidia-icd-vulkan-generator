using System.Text.Json;

namespace NvidiaICDVulkanGenerator
{
    public class CreateOrUpdateWorker : IWorker
    {
        private readonly IVulkanVersionProvider _vulkanVersionProvider;
        private readonly IFilePathProvider _filePathProvider;

        public CreateOrUpdateWorker(
            IVulkanVersionProvider vulkanVersionProvider,
            IFilePathProvider filePathProvider)
        {
            _vulkanVersionProvider = vulkanVersionProvider;
            _filePathProvider = filePathProvider;
        }

        public async Task Work()
        {
            var filePath = _filePathProvider.GetFilePath();

            try
            {
                System.Console.WriteLine($"Executing create/update on {filePath}");
                var vulkanVersion = _vulkanVersionProvider.GetVersion();
                var hasFile = File.Exists(filePath);

                if (!hasFile)
                {
                    System.Console.WriteLine(Helpers.GetFileNotFoundMessage(filePath));
                    System.Console.WriteLine("Creating it");

                    using var file = File.CreateText(filePath);
                    file.Write(JsonSerializer.Serialize(new JsonModel()));
                }


                var contents = JsonSerializer.Deserialize<JsonModel>(await File.ReadAllTextAsync(filePath), Options.JsonSerializerOptions);

                if (contents == null)
                {
                    throw new NullReferenceException();
                }

                var newModel = new JsonModel();
                newModel.ICD.APIVersion = vulkanVersion;

                if (newModel.ToString() == contents.ToString())
                {
                    System.Console.WriteLine($"{filePath} is up to date, no changes made to it.");
                    return;
                }

                contents.ICD.APIVersion = vulkanVersion;

                await File.WriteAllTextAsync(filePath, contents.ToJsonString());
                System.Console.WriteLine($"Successfully updated {filePath}");
            }
            catch (UnauthorizedAccessException)
            {
                System.Console.WriteLine($"The file {filePath} could not be updated due to lack of administrator priviledges. Run the command as root.");
                return;
            }
            catch (Exception)
            {
                System.Console.WriteLine($"Failed to parse {filePath}, please remove it so the program can recreate it.");
                return;
            }
        }
    }
}