using System.Text.Json;
using System.Text.Json.Serialization;

namespace NvidiaICDVulkanGenerator
{
    public class JsonModel
    {
        [JsonPropertyName("file_format_version")]
        public string FileFormatVersion { get; init; } = "";

        [JsonPropertyName("ICD")]
        public ICDModel ICD { get; init; } = new ICDModel();

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, Options.JsonSerializerOptions);
        }
    }

    public class ICDModel
    {
        [JsonPropertyName("library_path")]
        public string LibraryPath { get; init; } = "";

        [JsonPropertyName("api_version")]
        public string APIVersion { get; init; } = "";
    }
}