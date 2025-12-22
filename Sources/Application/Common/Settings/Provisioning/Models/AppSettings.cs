using JetBrains.Annotations;

namespace DrinkBuddy.Common.Settings.Provisioning.Models
{
    [PublicAPI]
    public class AppSettings
    {
        public const string SectionKey = "AppSettings";
        public string ConnectionString { get; set; } = null!;
        public string AppVersion { get; set; } = null!;
        public string GitHubCommit { get; set; } = null!;

        public required string OpenAiEndpoint { get; set; }
        public required string OpenAiKey { get; set; }
        public required string OpenAiDeploymentName { get; set; }
        public required string OpenAiModelId { get; set; }
    }
}