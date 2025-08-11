namespace NvidiaICDVulkanGenerator
{
    public class VulkanVersionNvidiaNotFoundException : Exception
    {
        public VulkanVersionNvidiaNotFoundException() : base("Vulkan version for NVIDIA not found (probably no NVIDIA adapter or missing driver?)") {}
    }
}