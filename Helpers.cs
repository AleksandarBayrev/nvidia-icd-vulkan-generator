using System.Diagnostics;

namespace NvidiaICDVulkanGenerator
{
    public static class Helpers
    {
        public static string GetFileNotFoundMessage(string path)
        {
            return $"Could not find {path}";
        }
        public static string GetCouldNotParseMessage(string path)
        {
            return $"Could not parse {path}";
        }
        public static string? ReadVulkanVersion()
        {
            using var process = new Process();
            process.StartInfo.FileName = "/usr/bin/vulkaninfo";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            var data = process.StandardOutput.ReadToEnd();
            process.Kill();

            return data.Split("\n").FirstOrDefault(x => x.Contains("vk_layer_nv", StringComparison.InvariantCultureIgnoreCase))?.Split("Vulkan version")?[1].Trim().Split(",")[0];
        }
    }
}