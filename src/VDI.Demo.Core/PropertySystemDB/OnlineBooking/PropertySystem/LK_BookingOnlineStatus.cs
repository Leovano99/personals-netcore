using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem
{
    [Table("LK_BookingOnlineStatus")]
    public class LK_BookingOnlineStatus : AuditedEntity
    {
        [Required]
        [StringLength(1)]
        public string statusType { get; set; }

        [Required]
        [StringLength(50)]
        public string statusTypeName { get; set; }
    }
}
