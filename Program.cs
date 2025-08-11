using System.Text;
using System.Text.Json;
using NvidiaICDVulkanGenerator;

if (!OperatingSystem.IsLinux())
{
    System.Console.WriteLine("This program is designed for Linux only!");
    return;
}

var vulkanVersion = Helpers.ReadVulkanVersion();

if (vulkanVersion == null)
{
    System.Console.WriteLine("Vulkan version for NVIDIA not found (probably no NVIDIA adapter or missing driver?)");
    return;
}

if (args.Length != 1 || !Constants.GetAvailableCommands().Contains(args[0]))
{
    Console.WriteLine($"Available args: {string.Join(", ", Constants.GetAvailableCommands())}");
    return;
}

if (args[0] == Constants.AvailableCommands.Read)
{
    var filePath = Path.Combine(Constants.BaseJsonPath, Constants.Filename);

    System.Console.WriteLine($"Executing read on {filePath}");

    var hasFile = File.Exists(filePath);
    if (!hasFile)
    {
        System.Console.WriteLine(Helpers.GetFileNotFoundMessage(filePath));
        return;
    }

    var json = JsonSerializer.Deserialize<JsonModel>(await File.ReadAllTextAsync(filePath), Options.JsonSerializerOptions);
    System.Console.WriteLine(json?.ToString() ?? Helpers.GetCouldNotParseMessage(filePath));
    return;
}

if (args[0] == Constants.AvailableCommands.CreateOrUpdate)
{
    var filePath = Path.Combine(Constants.BaseJsonPath, Constants.Filename);

    var hasFile = File.Exists(filePath);
    if (!hasFile)
    {
        System.Console.WriteLine(Helpers.GetFileNotFoundMessage(filePath));
        System.Console.WriteLine("Creating it");

        try
        {
            using var file = File.CreateText(filePath);
            file.Write(JsonSerializer.Serialize(new JsonModel()));
        }
        catch (UnauthorizedAccessException)
        {
            System.Console.WriteLine($"The file could not be created in {filePath} due to lack of administrator priviledges. Run the command as root.");
            return;
        }
    }

    var contents = JsonSerializer.Deserialize<JsonModel>(await File.ReadAllTextAsync(filePath), Options.JsonSerializerOptions);

    if (contents == null)
    {
        System.Console.WriteLine($"Failed to parse {filePath}");
        return;
    }

    var newModel = new JsonModel();
    newModel.ICD.APIVersion = vulkanVersion;

    if (newModel.ToString() == contents.ToString())
    {
        System.Console.WriteLine($"{filePath} is up to date, no changes made to it.");
        return;
    }

    contents.ICD.APIVersion = vulkanVersion;

    var sb = new StringBuilder(JsonSerializer.Serialize(contents, Options.JsonSerializerOptions));
    sb.Append(Environment.NewLine);
    await File.WriteAllTextAsync(filePath, sb.ToString());
    System.Console.WriteLine($"Successfully updated {filePath}");
}