using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LenelConfigService.Attributes;

namespace LenelConfigService.Models
{
    [Table("ExtractConfigurations")]
    public class ExtractConfiguration
    {
        // Primary Key
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        // Endpoint name
        [Required]
        [MaxLength(100)]
        [Column("EndpointName")]
        public string EndpointName { get; set; } = string.Empty;

        // Field toggles
        [Field, Column("EmplId"), JsonPropertyName("emplId")]
        public bool EmplId { get; set; }

        [Field, Column("BadgeId"), JsonPropertyName("badgeId")]
        public bool BadgeId { get; set; }

        [Field, Column("LastName"), JsonPropertyName("lastName")]
        public bool LastName { get; set; }

        [Field, Column("FirstName"), JsonPropertyName("firstName")]
        public bool FirstName { get; set; }

        [Field, Column("MiddleName"), JsonPropertyName("middleName")]
        public bool MiddleName { get; set; }

        [Field, Column("ContractNumber"), JsonPropertyName("contractNo")]
        public bool ContractNumber { get; set; }

        [Field, Column("NetId"), JsonPropertyName("netId")]
        public bool NetId { get; set; }

        [Field, Column("CompanyName"), JsonPropertyName("companyName")]
        public bool CompanyName { get; set; }

        [Field, Column("CreateProgram"), JsonPropertyName("createProgram")]
        public bool CreateProgram { get; set; }

        [Field, Column("CreateTimestamp"), JsonPropertyName("createTimestamp")]
        public bool CreateTimestamp { get; set; }

        [Field, Column("RowId"), JsonPropertyName("rowId")]
        public bool RowId { get; set; }

        [Field, Column("RowStamp"), JsonPropertyName("rowStamp")]
        public bool RowStamp { get; set; }

        [Field, Column("LastChanged"), JsonPropertyName("lastChanged")]
        public bool LastChanged { get; set; }

        [Field, Column("BadgeType"), JsonPropertyName("badgeType")]
        public bool BadgeType { get; set; }

        // Frequency
        [Column("Frequency")]
        public int Frequency { get; set; }

        // Endpoint URL
        [Column("Url")]
        public string? Url { get; set; }

        // Credentials
        [Column("CredentialUser")]
        public string? CredentialUser { get; set; }

        [Column("CredentialPassword")]
        public byte[]? CredentialPassword { get; set; }

        // Status
        [Column("Status")]
        public bool Status { get; set; } = true;
    }
}
