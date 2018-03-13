using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.TAXDB
{
    [Table("FP_TR_FPHeader")]
    public class FP_TR_FPHeader : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                  "-" + coCode +
                  "-" + FPCode;
            }
            set { /* nothing */ }
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string entityCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string coCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string FPCode { get; set; }

        [Required]
        [StringLength(8)]
        public string psCode { get; set; }

        [Required]
        [StringLength(2)]
        public string FPTransCode { get; set; }

        [Required]
        [StringLength(1)]
        public string FPStatCode { get; set; }

        [Required]
        [StringLength(3)]
        public string FPBranchCode { get; set; }

        [Required]
        [StringLength(2)]
        public string FPYear { get; set; }

        [Required]
        [StringLength(8)]
        public string FPNo { get; set; }

        [Required]
        [StringLength(1)]
        public string FPType { get; set; }

        [Required]
        [StringLength(16)]
        public string unitCode { get; set; }

        [Required]
        [StringLength(8)]
        public string unitNo { get; set; }

        public DateTime transDate { get; set; }

        [Column(TypeName = "money")]
        public decimal discAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal DPAmount { get; set; }

        [Required]
        [StringLength(3)]
        public string sourceCode { get; set; }

        [Column(TypeName = "money")]
        public decimal vatAmt { get; set; }

        [Required]
        [StringLength(1)]
        public string priceType { get; set; }

        [Required]
        [StringLength(5)]
        public string accCode { get; set; }

        [Required]
        [StringLength(20)]
        public string transNo { get; set; }

        [Required]
        [StringLength(10)]
        public string rentalCode { get; set; }

        [Required]
        [StringLength(13)]
        public string paymentCode { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(30)]
        public string NPWP { get; set; }

        [Required]
        [StringLength(200)]
        public string userAddress { get; set; }

        public int payNo { get; set; }

        [Required]
        [StringLength(15)]
        public string pmtBatchNo { get; set; }

        [Column(TypeName = "money")]
        public decimal? unitPriceAmt { get; set; }

        [Column(TypeName = "money")]
        public decimal? unitPriceVat { get; set; }

        public virtual ICollection<FP_TR_FPDetail> FP_TR_FPDetail { get; set; }
    }
}
