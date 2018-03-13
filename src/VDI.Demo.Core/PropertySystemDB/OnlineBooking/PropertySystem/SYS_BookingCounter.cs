using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem
{
    [Table("SYS_BookingCounter")]
    public class SYS_BookingCounter : AuditedEntity
    {
        public int entityID { get; set; }

        [Required]
        [StringLength(5)]
        public string coCode { get; set; }

        public int bookNo { get; set; }

        public int BASTNo { get; set; } 
    }
}
