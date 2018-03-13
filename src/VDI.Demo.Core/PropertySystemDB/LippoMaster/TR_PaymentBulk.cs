using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_PaymentBulk")]
    public class TR_PaymentBulk : AuditedEntity
    {

        [Required]
        [StringLength(150)]
        public string bulkPaymentKey { get; set; }
        
        [Required]
        [StringLength(8)]
        public string psCode { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public DateTime clearDate { get; set; }

        [Column(TypeName = "money")]
        public decimal amount { get; set; }

        [ForeignKey("TR_BookingHeader")]
        public int? bookingHeaderID { get; set; }
        public virtual TR_BookingHeader TR_BookingHeader { get; set; }

        [ForeignKey("MS_Unit")]
        public int unitID { get; set; }
        public virtual MS_Unit MS_Unit { get; set; }

        [ForeignKey("LK_PayFor")]
        public int payForID { get; set; }
        public virtual LK_PayFor LK_PayFor { get; set; }

        [ForeignKey("LK_PayType")]
        public int payTypeID { get; set; }
        public virtual LK_PayType LK_PayType { get; set; }

        [ForeignKey("LK_OthersType")]
        public int othersTypeID { get; set; }
        public virtual LK_OthersType LK_OthersType { get; set; }
    }
}
