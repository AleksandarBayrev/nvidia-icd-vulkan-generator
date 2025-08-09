using NvidiaICDVulkanGenerator;

var test = new JsonModel();
await File.WriteAllTextAsync(Constants.Filename, test.ToString());