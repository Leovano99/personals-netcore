using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem
{
    [Table("TR_UnitOrderDetail")]
    public class TR_UnitOrderDetail : AuditedEntity
    {
        [ForeignKey("TR_UnitOrderHeader")]
        public int UnitOrderHeaderID { get; set; }
        public virtual TR_UnitOrderHeader TR_UnitOrderHeader { get; set; }

        [ForeignKey("MS_Unit")]
        public int unitID { get; set; }
        public virtual MS_Unit MS_Unit { get; set; }

        [ForeignKey("MS_Renovation")]
        public int renovID { get; set; }
        public virtual MS_Renovation MS_Renovation { get; set; }

        [ForeignKey("MS_Term")]
        public int termID { get; set; }
        public virtual MS_Term MS_Term { get; set; }

        [Column(TypeName = "money")]
        public decimal BFAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal sellingPrice { get; set; }

        [StringLength(6)]
        public string PPNo { get; set; }

        [ForeignKey("TR_BookingHeader")]
        public int? bookingHeaderID { get; set; }
        public virtual TR_BookingHeader TR_BookingHeader { get; set; }

        //[StringLength(20)]
        //public string Bookcode { get; set; }

        [StringLength(300)]
        public string remarks { get; set; }

        public double? disc1 { get; set; }

        public double? disc2 { get; set; }

        [StringLength(100)]
        public string specialDiscType { get; set; }

        [StringLength(100)]
        public string groupBU { get; set; }
    }
}
