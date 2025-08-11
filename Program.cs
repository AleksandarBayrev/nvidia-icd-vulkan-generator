using NvidiaICDVulkanGenerator;

if (!OperatingSystem.IsLinux())
{
    System.Console.WriteLine("This program is designed for Linux only!");
    return;
}

if (args.Length != 1 || !Constants.GetAvailableCommands().Contains(args[0]))
{
    Console.WriteLine($"Available args: {string.Join(", ", Constants.GetAvailableCommands())}");
    return;
}
try
{
    IVulkanVersionProvider vulkanVersionProvider = new VulkanVersionProvider();

    var worker = Helpers.GetWorker(args[0], vulkanVersionProvider);

    await worker.Work();
}
catch (Exception ex)
{
    System.Console.WriteLine(ex.Message);
    return;
}