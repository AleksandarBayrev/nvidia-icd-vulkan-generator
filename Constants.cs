using System.Reflection;

namespace NvidiaICDVulkanGenerator
{
    public static class Constants
    {
        public const string BaseJsonPath = "/usr/share/vulkan/icd.d";
        public const string Filename = "nvidia_icd.json";
        public static class AvailableCommands
        {
            public const string Read = "--read";
            public const string CreateOrUpdate = "--create-or-update";
        }

        public static IEnumerable<string> GetAvailableCommands()
        {
            var constantsType = typeof(AvailableCommands);
            return constantsType.GetFields(
                BindingFlags.Public
                | BindingFlags.Static
                | BindingFlags.FlattenHierarchy
                )
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly).Select(x => x.GetRawConstantValue()?.ToString() ?? "")
                .Where(x => x.Length != 0);
        }
    }
}