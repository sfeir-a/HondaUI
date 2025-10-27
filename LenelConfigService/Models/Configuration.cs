using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LenelConfigService.Attributes;

namespace LenelConfigService.Models
{
    [Table("Configurations")]
    public class Configuration
    {
        [Key]
        [Column("Endpoint_name")]
        [Required(ErrorMessage = "Application name is required.")]
        [MaxLength(255)]
        public string EndpointName { get; set; } = string.Empty;

        [Field, Column("EMPLID"), JsonPropertyName("emplId")]
        public bool EmplId { get; set; }

        [Field, Column("BADGEID"), JsonPropertyName("badgeId")]
        public bool BadgeId { get; set; }

        [Field, Column("LAST_NAME"), JsonPropertyName("lastName")]
        public bool LastName { get; set; }

        [Field, Column("FIRST_NAME"), JsonPropertyName("firstName")]
        public bool FirstName { get; set; }

        [Field, Column("MIDDLE_NAME"), JsonPropertyName("middleName")]
        public bool MiddleName { get; set; }

        [Field, Column("CONTR_NO"), JsonPropertyName("contractNo")]
        public bool ContractNo { get; set; }

        [Field, Column("NET_ID"), JsonPropertyName("netId")]
        public bool NetId { get; set; }

        [Field, Column("COMPANY_NAME"), JsonPropertyName("companyName")]
        public bool CompanyName { get; set; }

        [Field, Column("CREATE_PROGRAM"), JsonPropertyName("createProgram")]
        public bool CreateProgram { get; set; }

        [Field, Column("CREATE_TSTP"), JsonPropertyName("createTimestamp")]
        public bool CreateTimestamp { get; set; }

        [Field, Column("ROWID"), JsonPropertyName("rowId")]
        public bool RowId { get; set; }

        [Field, Column("ROWSTAMP"), JsonPropertyName("rowStamp")]
        public bool RowStamp { get; set; }

        [Field, Column("lastchanged"), JsonPropertyName("lastChanged")]
        public bool LastChanged { get; set; }

        [Field, Column("BadgeType"), JsonPropertyName("badgeType")]
        public bool BadgeType { get; set; }

        [Column("Frequency")]
        public int Frequency { get; set; }

        [Column("URL")]
        public string? Url { get; set; }
    }
}
