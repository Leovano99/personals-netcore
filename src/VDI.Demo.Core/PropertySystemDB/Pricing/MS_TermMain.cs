using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.PropertySystemDB.Pricing
{
    [Table("MS_TermMain")]
    public class MS_TermMain : AuditedEntity
    {
        [StringLength(5)]
        public string termCode { get; set; }

        public int entityID { get; set; }

        [Required]
        [StringLength(200)]
        public string termDesc { get; set; }

        [Required]
        [StringLength(5)]
        public string famDiscCode { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal BFAmount { get; set; }

        [Required]
        [StringLength(200)]
        public string remarks { get; set; }

        public ICollection<MS_Term> MS_Term { get; set; }

        public ICollection<MS_Unit> MS_Unit { get; set; }

    }
}
