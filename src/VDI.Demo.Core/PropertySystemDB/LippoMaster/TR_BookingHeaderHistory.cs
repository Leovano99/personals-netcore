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
    [Table("TR_BookingHeaderHistory")]
    public class TR_BookingHeaderHistory : AuditedEntity
    {
        public int entityID { get; set; }

        [Required]
        [StringLength(20)]
        public string bookCode { get; set; }
 
        public int unitID { get; set; }

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

        public int eventID { get; set; } 
 
        public int transID { get; set; } 

        [Required]
        [StringLength(3)]
        public string BFPayTypeCode { get; set; }

        [Required]
        [StringLength(30)]
        public string bankNo { get; set; }

        [Required]
        [StringLength(50)]
        public string bankName { get; set; }

        public int termID { get; set; }

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

        public int shopBusinessID { get; set; }

        public int SADStatusID { get; set; }

        public int promotionID { get; set; }

        [Required]
        [StringLength(1)]
        public string discBFCalcType { get; set; }

        [Required]
        [StringLength(1)]
        public string DPCalcType { get; set; }

        public int? sumberDanaID { get; set; }

        public int? tujuanTransaksiID { get; set; }

        [StringLength(50)]
        public string nomorRekeningPemilik { get; set; }

        [StringLength(50)]
        public string bankRekeningPemilik { get; set; }
        
        public int? facadeID { get; set; }

        public byte historyNo { get; set; }

    }
}
