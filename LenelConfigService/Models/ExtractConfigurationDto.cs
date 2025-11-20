using System.Text.Json.Serialization;

namespace LenelConfigService.Models
{
    public class ExtractConfigurationDto
    {
        public int Id { get; set; }
        public string EndpointName { get; set; } = string.Empty;
        public int Frequency { get; set; }
        public string? Url { get; set; }
        public bool Status { get; set; }

        public string? CredentialUser { get; set; }
        public string? CredentialPassword { get; set; }
        public bool HasCredentialPassword { get; set; }

        // List of active fields for UI
        public string[] ActiveFields { get; set; } = Array.Empty<string>();
    }
}
