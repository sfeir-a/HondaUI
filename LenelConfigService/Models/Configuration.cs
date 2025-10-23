using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LenelConfigService.Models
{
    [Table("Configurations")]
    public class Configuration
    {
        [Key]
        [Column("Endpoint_name")]
        [MaxLength(255)]
        public string EndpointName { get; set; } = string.Empty;

        [Column("EMPLID")]
        public bool EmplId { get; set; }

        [Column("BADGEID")]
        public bool BadgeId { get; set; }

        [Column("LAST_NAME")]
        public bool LastName { get; set; }

        [Column("FIRST_NAME")]
        public bool FirstName { get; set; }

        [Column("MIDDLE_NAME")]
        public bool MiddleName { get; set; }

        [Column("CONTR_NO")]
        public bool ContractNo { get; set; }

        [Column("NET_ID")]
        public bool NetId { get; set; }

        [Column("COMPANY_NAME")]
        public bool CompanyName { get; set; }

        [Column("CREATE_PROGRAM")]
        public bool CreateProgram { get; set; }

        [Column("CREATE_TSTP")]
        public bool CreateTimestamp { get; set; }

        [Column("ROWID")]
        public bool RowId { get; set; }

        [Column("ROWSTAMP")]
        public bool RowStamp { get; set; }

        [Column("lastchanged")]
        public bool LastChanged { get; set; }

        [Column("BadgeType")]
        public bool BadgeType { get; set; }

        [Column("Frequency")]
        public int Frequency { get; set; }

        [Column("URL")]
        public string? Url { get; set; }
    }
}
