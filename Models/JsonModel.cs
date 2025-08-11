using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NvidiaICDVulkanGenerator
{
    public class JsonModel
    {
        public JsonModel()
        {
            this.ICD = new ICDModel();
        }

        public JsonModel(string apiVersion)
        {
            this.ICD = new ICDModel(apiVersion);
        }

        [JsonPropertyOrder(1)]
        [JsonPropertyName("file_format_version")]
        public string FileFormatVersion { get; init; } = "1.0.0";

        [JsonPropertyOrder(0)]
        [JsonPropertyName("ICD")]
        public ICDModel ICD { get; init; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, Options.JsonSerializerOptions);
        }

        public string ToJsonString()
        {
            var sb = new StringBuilder(JsonSerializer.Serialize(this, Options.JsonSerializerOptions));
            sb.Append(Environment.NewLine);
            return sb.ToString();
        }
    }

    public class ICDModel
    {
        public ICDModel()
        {
            this.APIVersion = "";
        }

        public ICDModel(string apiVersion)
        {
            this.APIVersion = apiVersion;
        }

        [JsonPropertyOrder(1)]
        [JsonPropertyName("library_path")]
        public string LibraryPath
        {
            get
            {
                var olderCPUArch = "/usr/lib/i386-linux-gnu/libGLX_nvidia.so.0";
                var newerCPUArch = "/usr/lib/x86_64-linux-gnu/libGLX_nvidia.so.0";
                return Environment.Is64BitOperatingSystem ? newerCPUArch : olderCPUArch;
            }
        }

        [JsonPropertyOrder(0)]
        [JsonPropertyName("api_version")]
        public string APIVersion { get; set; } = "";
    }
}