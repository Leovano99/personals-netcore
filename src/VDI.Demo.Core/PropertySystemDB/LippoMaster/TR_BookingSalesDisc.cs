using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_BookingSalesDisc")]
    public partial class TR_BookingSalesDisc : AuditedEntity
    {
        [ForeignKey("TR_BookingHeader")]
        public int bookingHeaderID { get; set; }
        public virtual TR_BookingHeader TR_BookingHeader { get; set; }

        //[Required]
        //[StringLength(20)]
        //public string bookCode { get; set; }

        [ForeignKey("LK_Item")]
        public int itemID { get; set; }
        public virtual LK_Item LK_Item { get; set; }

        public double pctDisc { get; set; }
        public double pctTax { get; set; }
    }
}
