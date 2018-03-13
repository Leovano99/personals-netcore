using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.PPOnline
{
    [Table("LK_PaymentType")]
    public class LK_PaymentType : AuditedEntity
    {
        public int paymentType { get; set; }

        [Required]
        [StringLength(50)]
        public string paymentTypeName { get; set; }

        public short sortNo { get; set; }
    }
}
