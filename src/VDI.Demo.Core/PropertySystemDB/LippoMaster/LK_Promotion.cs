using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("LK_Promotion")]
    public class LK_Promotion : AuditedEntity
    {
        [Required]
        [StringLength(3)]
        public string promotionCode { get; set; }

        [Required]
        [StringLength(100)]
        public string promotionDesc { get; set; }

        public virtual ICollection<TR_BookingHeader> TR_BookingHeader { get; set; }
    }
}
