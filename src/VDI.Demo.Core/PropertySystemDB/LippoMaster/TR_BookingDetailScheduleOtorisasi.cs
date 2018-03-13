using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_BookingDetailScheduleOtorisasi")]
    public class TR_BookingDetailScheduleOtorisasi : AuditedEntity
    {
        public int entityID { get; set; }

        //[StringLength(20)]
        //public string bookCode { get; set; }
        //public short refNo { get; set; }

        [ForeignKey("TR_BookingDetail")]
        public int bookingDetailID { get; set; }
        public virtual TR_BookingDetail TR_BookingDetail { get; set; }

        public short schedNo { get; set; }

        public DateTime dueDate { get; set; }

        //[Required]
        //[StringLength(3)]
        //public string allocCode { get; set; }

        [ForeignKey("LK_Alloc")]
        public int allocID { get; set; }
        public virtual LK_Alloc LK_Alloc { get; set; }

        [Column(TypeName = "money")]
        public decimal netAmt { get; set; }

        [Column(TypeName = "money")]
        public decimal vatAmt { get; set; }

    }
}
