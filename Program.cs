using System.Text.Json;
using NvidiaICDVulkanGenerator;

if (!OperatingSystem.IsLinux())
{
    System.Console.WriteLine("This program is designed for Linux only!");
    return;
}

IVulkanVersionProvider vulkanVersionProvider = new VulkanVersionProvider();

var vulkanVersion = vulkanVersionProvider.GetVersion();

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
    var worker = new ReadWorker();
    await worker.Work();
    return;
}

if (args[0] == Constants.AvailableCommands.CreateOrUpdate)
{
    var worker = new CreateOrUpdateWorker(vulkanVersionProvider);
}