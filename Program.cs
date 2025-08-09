using System.Text.Json;
using NvidiaICDVulkanGenerator;

System.Console.WriteLine(string.Join(",", args));
System.Console.WriteLine(Helpers.ReadVulkanVersion());
if (args.Length == 0)
{
    Console.WriteLine("Available args: --read, --create");
    return;
}

if (args[0] == "--read")
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

if (args[0] == "--create")
{
    System.Console.WriteLine("Command is WIP");
}