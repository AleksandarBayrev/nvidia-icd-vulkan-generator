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


    try
    {
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