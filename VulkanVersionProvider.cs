using System.Diagnostics;

namespace NvidiaICDVulkanGenerator
{
    public class VulkanVersionProvider : IVulkanVersionProvider
    {
        public string GetVersion()
        {
            using var process = new Process();
            process.StartInfo.FileName = "/usr/bin/vulkaninfo";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();

            var data = process.StandardOutput.ReadToEnd();

            process.Kill();

            var parsedData = data.Split("\n").FirstOrDefault(x => x.Contains("vk_layer_nv", StringComparison.InvariantCultureIgnoreCase))?.Split("Vulkan version")?[1].Trim().Split(",")[0];

            if (string.IsNullOrEmpty(parsedData))
            {
                throw new VulkanVersionNvidiaNotFoundException();
            }

            return parsedData;
        }
    }
}