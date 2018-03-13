using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_BookingTax")]
    public class TR_BookingTax : AuditedEntity
    {
        [ForeignKey("TR_BookingDetail")]
        public int bookingDetailID { get; set; }
        public virtual TR_BookingDetail TR_BookingDetail { get; set; }

        [ForeignKey("MS_TaxType")]
        public int taxTypeID { get; set; }
        public virtual MS_TaxType MS_TaxType { get; set; }

        [Column(TypeName = "money")]
        public decimal amount { get; set; }
    }
}
