using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("LK_Reason")]
    public class LK_Reason : AuditedEntity
    {
        public int entityID { get; set; }

        [Required]
        [StringLength(2)]
        public string reasonCode { get; set; }

        [StringLength(50)]
        public string reasonDesc { get; set; }

        public bool? isActive { get; set; }

        public virtual ICollection<TR_BookingCancel> TR_BookingCancel { get; set; }
    }
}
