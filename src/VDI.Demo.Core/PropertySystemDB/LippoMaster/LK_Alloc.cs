using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("LK_Alloc")]
    public class LK_Alloc : AuditedEntity
    {
        public int entityID { get; set; }

        [Required]
        [StringLength(3)]
        public string allocCode { get; set; }

        [Required]
        [StringLength(30)]
        public string allocDesc { get; set; }

        public bool isVAT { get; set; }

        public bool isActive { get; set; }

        [ForeignKey("LK_PayFor")]
        public int? payForID { get; set; }
        public virtual LK_PayFor LK_PayFor { get; set; }

        public ICollection<TR_BookingDetailSchedule> TR_BookingDetailSchedule { get; set; }
        public ICollection<TR_BookingDetailScheduleOtorisasi> TR_BookingDetailScheduleOtorisasi { get; set; }
    }
}
