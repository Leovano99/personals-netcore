using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem;

namespace VDI.Demo.PropertySystemDB.Pricing
{
    [Table("MS_Term")]
    public class MS_Term : AuditedEntity
    {
        public int entityID { get; set; }

        //unique
        [StringLength(5)]
        public string termCode { get; set; }

        public short termNo { get; set; }

        public short PPJBDue { get; set; }

        [Required]
        [StringLength(200)]
        public string remarks { get; set; }

        public int? termInstallment { get; set; }

        public bool isActive { get; set; }

        public int sortNo { get; set; }

        [Required]
        [StringLength(1)]
        public string discBFCalcType { get; set; }

        [Required]
        [StringLength(1)]
        public string DPCalcType { get; set; }

        [StringLength(5)]
        public string GroupTermCode { get; set; }

        [ForeignKey("MS_Project")]
        public int projectID { get; set; }
        public virtual MS_Project MS_Project { get; set; }

        [ForeignKey("MS_TermMain")]
        public int termMainID { get; set; }
        public virtual MS_TermMain MS_TermMain { get; set; }

        public ICollection<MS_TermAddDisc> MS_TermAddDisc { get; set; }

        public ICollection<MS_TermDP> MS_TermDP { get; set; }

        public ICollection<MS_TermPmt> MS_TermPmt { get; set; }

        public ICollection<TR_BookingHeader> TR_BookingHeader { get; set; }

        public virtual ICollection<TR_BookingHeaderTerm> TR_BookingHeaderTerm { get; set; }

        public ICollection<TR_UnitReserved> TR_UnitReserved { get; set; }

        public ICollection<TR_UnitOrderDetail> TR_UnitOrderDetail { get; set; }

        public ICollection<TR_BookingItemPrice> TR_BookingItemPrice { get; set; }

        public ICollection<MS_TermDiscOnlineBooking> MS_TermDiscOnlineBooking { get; set; }

        public ICollection<MS_UnitItemPrice> MS_UnitItemPrice { get; set; }

    }
}
