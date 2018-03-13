using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    [Table("MS_Unit")]
    public class MS_Unit : AuditedEntity
    {

        //unique
        [Required]
        [StringLength(8)]
        public string unitNo { get; set; }

        [Required]
        [StringLength(8)]
        public string CombinedUnitNo { get; set; }

        [Required]
        [StringLength(1)]
        public string unitCertCode { get; set; }

        [Required]
        [StringLength(100)]
        public string remarks { get; set; }

        [Required]
        [StringLength(8)]
        public string prevUnitNo { get; set; }

        public int entityID { get; set; }

        [ForeignKey("MS_UnitCode")]
        public int unitCodeID { get; set; }
        public virtual MS_UnitCode MS_UnitCode { get; set; }

        [ForeignKey("MS_Area")]
        public int areaID { get; set; }
        public virtual MS_Area MS_Area { get; set; }

        [ForeignKey("MS_Project")]
        public int projectID { get; set; }
        public virtual MS_Project MS_Project { get; set; }

        [ForeignKey("MS_Category")]
        public int categoryID { get; set; }
        public virtual MS_Category MS_Category { get; set; }

        [ForeignKey("MS_Cluster")]
        public int clusterID { get; set; }
        public virtual MS_Cluster MS_Cluster { get; set; }

        [ForeignKey("MS_Product")]
        public int productID { get; set; }
        public virtual MS_Product MS_Product { get; set; }

        [ForeignKey("MS_Detail")]
        public int detailID { get; set; }
        public virtual MS_Detail MS_Detail { get; set; }

        [ForeignKey("MS_Zoning")]
        public int zoningID { get; set; }
        public virtual MS_Zoning MS_Zoning { get; set; }

        [ForeignKey("LK_Facing")]
        public int facingID { get; set; }
        public virtual LK_Facing LK_Facing { get; set; }

        [ForeignKey("LK_UnitStatus")]
        public int unitStatusID { get; set; }
        public virtual LK_UnitStatus LK_UnitStatus { get; set; }
        
        [ForeignKey("LK_RentalStatus")]
        public int rentalStatusID { get; set; }
        public virtual LK_RentalStatus LK_RentalStatus { get; set; }

        [ForeignKey("MS_TermMain")]
        public int? termMainID { get; set; }
        public virtual MS_TermMain MS_TermMain { get; set; }

        public int? TokenNo { get; set; }

        public ICollection<MS_UnitItem> MS_UnitItem { get; set; }

        public ICollection<TR_BookingHeader> TR_BookingHeader { get; set; }

        public ICollection<TR_UnitOrderDetail> TR_UnitOrderDetail { get; set; }

        public ICollection<TR_UnitReserved> TR_UnitReserved { get; set; }

        public ICollection<MS_UnitItemPrice> MS_UnitItemPrice { get; set; }

        public virtual ICollection<TR_PaymentBulk> TR_PaymentBulk { get; set; }
    }
}
