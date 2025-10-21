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
        public bool EMPLID { get; set; }
        public bool BADGEID { get; set; }
        public bool LAST_NAME { get; set; }
        public bool FIRST_NAME { get; set; }
        public bool MIDDLE_NAME { get; set; }
        public bool CONTR_NO { get; set; }
        public bool NET_ID { get; set; }
        public bool COMPANY_NAME { get; set; }
        public bool CREATE_PROGRAM { get; set; }
        public bool CREATE_TSTP { get; set; }
        public bool ROWID { get; set; }
        public bool ROWSTAMP { get; set; }
        public bool lastchanged { get; set; }
        public bool BadgeType { get; set; }
        public int Frequency { get; set; }
        public string? URL { get; set; }
    }
}
