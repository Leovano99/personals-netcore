using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_BookingHeader")]
    public class TR_BookingHeader : AuditedEntity
    {
        public int entityID { get; set; }

        [Required]
        [StringLength(20)]
        public string bookCode { get; set; }

        [ForeignKey("MS_Unit")]
        public int unitID { get; set; }
        public virtual MS_Unit MS_Unit { get; set; }

        public DateTime bookDate { get; set; }

        public DateTime? cancelDate { get; set; }

        [Required]
        [StringLength(8)]
        public string psCode { get; set; }

        [Required]
        [StringLength(3)]
        public string scmCode { get; set; }

        [Required]
        [StringLength(12)]
        public string memberCode { get; set; }

        [Required]
        [StringLength(100)]
        public string memberName { get; set; }

        [Required]
        [StringLength(20)]
        public string NUP { get; set; }

        public bool? isSK { get; set; }

        [ForeignKey("MS_SalesEvent")]
        public int eventID { get; set; }
        public virtual MS_SalesEvent MS_SalesEvent { get; set; }

        [ForeignKey("MS_Transfrom")]
        public int transID { get; set; }
        public virtual MS_TransFrom MS_TransFrom { get; set; }

        [Required]
        [StringLength(3)]
        public string BFPayTypeCode { get; set; }

        [Required]
        [StringLength(30)]
        public string bankNo { get; set; }

        [Required]
        [StringLength(50)]
        public string bankName { get; set; }

        //public int termID { get; set; }
        [ForeignKey("MS_Term")]
        public int termID { get; set; }
        public virtual MS_Term MS_Term { get; set; }

        //public short termNo { get; set; }

        public short PPJBDue { get; set; }

        [Required]
        [StringLength(200)]
        public string termRemarks { get; set; }

        [Required]
        [StringLength(1500)]
        public string remarks { get; set; }

        [Column(TypeName = "money")]
        public decimal netPriceComm { get; set; }

        [Required]
        [StringLength(5)]
        public string KPRBankCode { get; set; }

        public bool isPenaltyStop { get; set; }

        public bool isSMS { get; set; }
        
        //public int shopBusinessID { get; set; }

        [ForeignKey("MS_ShopBusiness")]
        public int shopBusinessID { get; set; }
        public virtual MS_ShopBusiness MS_ShopBusiness { get; set; }

        [ForeignKey("LK_SADStatus")]
        public int SADStatusID { get; set; }
        public virtual LK_SADStatus LK_SADStatus { get; set; }

        [ForeignKey("LK_Promotion")]
        public int promotionID { get; set; }
        public virtual LK_Promotion LK_Promotion { get; set; }

        [Required]
        [StringLength(1)]
        public string discBFCalcType { get; set; }

        [Required]
        [StringLength(1)]
        public string DPCalcType { get; set; }

        [ForeignKey("MS_SumberDana")]
        public int? sumberDanaID { get; set; }
        public virtual MS_SumberDana MS_SumberDana { get; set; }
        
        [ForeignKey("MS_TujuanTransaksi")]
        public int? tujuanTransaksiID { get; set; }
        public virtual MS_TujuanTransaksi MS_TujuanTransaksi { get; set; }

        [StringLength(50)]
        public string nomorRekeningPemilik { get; set; }

        [StringLength(50)]
        public string bankRekeningPemilik { get; set; }
        
        [ForeignKey("MS_Facade")]
        public int? facadeID { get; set; }
        public virtual MS_Facade MS_Facade { get; set; }

        public virtual ICollection<TR_BookingDetail> TR_BookingDetail { get; set; }

        public virtual ICollection<TR_PenaltySchedule> TR_PenaltySchedule { get; set; }

        public virtual ICollection<TR_BookingChangeOwner> TR_BookingChangeOwner { get; set; }

        public virtual ICollection<TR_BookingCorres> TR_BookingCorres { get; set; }

        public virtual ICollection<TR_PaymentHeader> TR_PaymentHeader { get; set; }

        public virtual ICollection<TR_CommAddDisc> TR_CommAddDisc { get; set; }

        public virtual ICollection<TR_BookingSalesDisc> TR_BookingSalesDisc { get; set; }

        public virtual ICollection<TR_ReminderLetter> TR_ReminderLetter { get; set; }

        public virtual ICollection<TR_BookingDocument> TR_BookingDocument { get; set; }

        public virtual ICollection<TR_BookingCancel> TR_BookingCancel { get; set; }

        public virtual ICollection<TR_BookingItemPrice> TR_BookingItemPrice { get; set; }

        public virtual ICollection<TR_UnitOrderDetail> TR_UnitOrderDetail { get; set; }

        public virtual ICollection<TR_BookingHeaderTerm> TR_BookingHeaderTerm { get; set; }

        public virtual ICollection<TR_PaymentBulk> TR_PaymentBulk { get; set; }

    }
}
