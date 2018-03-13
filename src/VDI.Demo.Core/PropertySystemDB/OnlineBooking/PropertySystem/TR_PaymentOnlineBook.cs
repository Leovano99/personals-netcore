using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem
{
    [Table("TR_PaymentOnlineBook")]
    public class TR_PaymentOnlineBook : AuditedEntity
    {
        [ForeignKey("TR_UnitOrderHeader")]
        public int UnitOrderHeaderID { get; set; }
        public virtual TR_UnitOrderHeader TR_UnitOrderHeader { get; set; }

        public DateTime paymentDate { get; set; }

        [Required]
        [StringLength(200)]
        public string paymentType { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal paymentAmt { get; set; }
        
        [StringLength(100)]
        public string bankName { get; set; }

        [StringLength(100)]
        public string bankAccName { get; set; }

        [StringLength(100)]
        public string bankAccNo { get; set; }

        [StringLength(300)]
        public string resiImage { get; set; }

        [StringLength(200)]
        public string resiNo { get; set; }

        [StringLength(100)]
        public string offlineType { get; set; }
    }
}
