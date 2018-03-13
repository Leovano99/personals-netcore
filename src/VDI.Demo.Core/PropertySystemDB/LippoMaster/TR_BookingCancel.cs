using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_BookingCancel")]
    public class TR_BookingCancel : AuditedEntity
    {
        public int entityID { get; set; }

        [ForeignKey("TR_BookingHeader")]
        public int bookingHeaderID { get; set; }
        public virtual TR_BookingHeader TR_BookingHeader { get; set; }

        //[Required]
        //[StringLength(20)]
        //public string bookCode { get; set; }

        //[StringLength(2)]
        //public string reasonCode { get; set; }

        [ForeignKey("LK_Reason")]
        public int reasonID { get; set; }
        public virtual LK_Reason LK_Reason { get; set; }

        public DateTime? cancelDate { get; set; }

        [Column(TypeName = "money")]
        public decimal lostAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal refundAmount { get; set; }

        [Required]
        [StringLength(500)]
        public string remarks { get; set; }

        [Required]
        [StringLength(20)]
        public string newBookCode { get; set; }

    }
}
