using System.Text.Json;
using NvidiaICDVulkanGenerator;

if (args.FirstOrDefault(x => x.Contains("--read")) != null)
{
    var filePath = Path.Combine(Constants.BaseJsonPath, Constants.Filename);
    System.Console.WriteLine($"Executing read on {filePath}");
    var test = new JsonModel();
    var json = JsonSerializer.Deserialize<JsonModel>(await File.ReadAllTextAsync(filePath), Options.JsonSerializerOptions);
    System.Console.WriteLine(json.ToString());
    return;
}

if (args.FirstOrDefault(x => x.Contains("--create")) != null)
{
    System.Console.WriteLine("Command is WIP");
}