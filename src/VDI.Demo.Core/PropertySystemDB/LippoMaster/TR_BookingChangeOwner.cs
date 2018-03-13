using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_BookingChangeOwner")]
    public class TR_BookingChangeOwner : AuditedEntity
    {
        public int entityID { get; set; }

        [ForeignKey("TR_BookingHeader")]
        public int bookingHeaderID { get; set; }
        public virtual TR_BookingHeader TR_BookingHeader { get; set; }

        //[Required]
        //[StringLength(20)]
        //public string bookCode { get; set; }
        
        public int seqNo { get; set; }

        [Required]
        [StringLength(30)]
        public string docNo { get; set; }

        public DateTime? ADDNDate { get; set; }

        [Required]
        [StringLength(30)]
        public string ADDNNo { get; set; }
        
        public double costPct { get; set; }

        [Column(TypeName = "money")]
        public decimal costAmt { get; set; }

        [Required]
        [StringLength(8)]
        public string oldPsCode { get; set; }

        [Required]
        [StringLength(8)]
        public string newPsCode { get; set; }

        [Required]
        [StringLength(4)]
        public string oldFinType { get; set; }

        [Required]
        [StringLength(4)]
        public string newFinType { get; set; }

        [Required]
        [StringLength(5)]
        public string oldBankCode { get; set; }

        [Required]
        [StringLength(5)]
        public string newBankCode { get; set; }

        [Required]
        [StringLength(100)]
        public string remarks { get; set; }

        [StringLength(50)]
        public string noObjekPajak { get; set; }

        [Column(TypeName = "money")]
        public decimal? nilaiJualObjekPajakTanah { get; set; }

        [Column(TypeName = "money")]
        public decimal? nilaiJualObjekPajakBangunan { get; set; }

        [Column(TypeName = "money")]
        public decimal? nilaiPengalihan { get; set; }

        [StringLength(50)]
        public string noTandaPenerimaanNegara { get; set; }

        public DateTime? tanggalPenyetoran { get; set; }

        [Column(TypeName = "money")]
        public decimal? jumlahSetoran { get; set; }
    }
}
