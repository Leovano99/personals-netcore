using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDI.Demo.PropertySystemDB.Pricing
{
    [Table("MS_TermDP")]
    public class MS_TermDP : AuditedEntity
    {
        [StringLength(5)]
        public string termCode { get; set; }

        public int entityID { get; set; }

        [ForeignKey("MS_Term")]
        public int termID { get; set; }
        public virtual MS_Term MS_Term { get; set; }

        public short termNo { get; set; }

        public byte DPNo { get; set; }

        public short daysDue { get; set; }

        public double DPPct { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal DPAmount { get; set; }

        public int? daysDueNewKP { get; set; }
    }
}
