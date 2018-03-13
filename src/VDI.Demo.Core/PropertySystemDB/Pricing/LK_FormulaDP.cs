using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VDI.Demo.PropertySystemDB.LippoMaster;

namespace VDI.Demo.PropertySystemDB.Pricing
{
    [Table("LK_FormulaDP")]
    public class LK_FormulaDP : AuditedEntity
    {
        [Required]
        [StringLength(5)]
        public string formulaDPType { get; set; }

        [Required]
        [StringLength(100)]
        public string formulaDPDesc { get; set; }

        public ICollection<TR_BookingDetailDP> TR_BookingDetailDP { get; set; }
    }
}
