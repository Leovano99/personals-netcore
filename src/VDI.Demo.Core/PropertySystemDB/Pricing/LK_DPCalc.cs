using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VDI.Demo.PropertySystemDB.LippoMaster;

namespace VDI.Demo.PropertySystemDB.Pricing
{
    public class LK_DPCalc : AuditedEntity
    {
        [Required]
        [StringLength(5)]
        public string DPCalcType { get; set; }

        [Required]
        [StringLength(100)]
        public string DPCalcDesc { get; set; }

        public ICollection<TR_BookingDetailDP> TR_BookingDetailDP { get; set; }
    }
}
