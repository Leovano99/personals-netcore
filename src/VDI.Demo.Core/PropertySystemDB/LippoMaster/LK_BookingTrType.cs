using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("LK_BookingTrType")]
    public class LK_BookingTrType : AuditedEntity
    {
        [Required]
        [StringLength(2)]
        public string bookingTrType { get; set; }

        [Required]
        [StringLength(40)]
        public string ket { get; set; }

        public ICollection<TR_BookingDetail> TR_BookingDetail { get; set; }
    }
}
