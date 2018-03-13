using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_BookingDetail")]
    public class TR_BookingDetail : AuditedEntity
    {
        public int entityID { get; set; }
        
        [ForeignKey("TR_BookingHeader")]
        public int bookingHeaderID { get; set; }
        public virtual TR_BookingHeader TR_BookingHeader { get; set; }

        //[Required]
        //[StringLength(20)]
        //public string bookCode { get; set; }

        public short refNo { get; set; }

        //[Required]
        //[StringLength(2)]
        //public string trType { get; set; }

        [ForeignKey("LK_BookingTrType")]
        public int bookingTrTypeID { get; set; }
        public virtual LK_BookingTrType LK_BookingTrType { get; set; }

        //public int unitID { get; set; }

        //public int itemID { get; set; }

        [ForeignKey("LK_Item")]
        public int itemID { get; set; }
        public virtual LK_Item LK_Item { get; set; }

        [Required]
        [StringLength(5)]
        public string coCode { get; set; }

        public int bookNo { get; set; }

        [Column(TypeName = "money")]
        public decimal BFAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal amount { get; set; }

        public double pctDisc { get; set; }

        public double pctTax { get; set; }

        public double area { get; set; }

        //public int termNo { get; set; }
        
        public int finTypeID { get; set; }

        //public short finStartDue { get; set; }

        [Required]
        [StringLength(1)]
        public string combineCode { get; set; }

        [Column(TypeName = "money")]
        public decimal amountComm { get; set; }

        [Column(TypeName = "money")]
        public decimal netPriceComm { get; set; }

        [Column(TypeName = "money")]
        public decimal amountMKT { get; set; }

        [Column(TypeName = "money")]
        public decimal netPriceMKT { get; set; }

        [Column(TypeName = "money")]
        public decimal netPriceCash { get; set; }

        [Column(TypeName = "money")]
        public decimal netPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal netNetPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal adjPrice { get; set; }

        public double adjArea { get; set; }

        public virtual ICollection<TR_CommAddDisc> TR_CommAddDisc { get; set; }

        public virtual ICollection<TR_SSPDetail> TR_SSPDetail { get; set; }

        public virtual ICollection<TR_BookingDetailAddDisc> TR_BookingDetailAddDisc { get; set; }

        public virtual ICollection<TR_BookingDetailSchedule> TR_BookingDetailSchedule { get; set; }

        public virtual ICollection<TR_BookingDetailAdjustment> TR_BookingDetailAdjustment { get; set; }

        public virtual ICollection<TR_BookingDetailScheduleOtorisasi> TR_BookingDetailScheduleOtorisasi { get; set; }

        public virtual ICollection<TR_BookingDetailDP> TR_BookingDetailDP { get; set; }

        public virtual ICollection<TR_BookingTax> TR_BookingTax { get; set; }

        public virtual ICollection<TR_CashAddDisc> TR_CashAddDisc { get; set; }

        public virtual ICollection<TR_MKTAddDisc> TR_MKTAddDisc { get; set; }

        public virtual ICollection<TR_DPHistory> TR_DPHistory { get; set; }

    }
}
