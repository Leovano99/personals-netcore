using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.NewCommDB
{
    [Table("TR_ManagementFee")]
    public class TR_ManagementFee : Entity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                    "-" + scmCode +
                    "-" + propCode;
            }
            set { /* nothing */ }
        }
        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string entityCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string scmCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string propCode { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(5)]
        public string devCode { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(20)]
        public string bookNo { get; set; }

        [Key]
        [Column(Order = 5)]
        public int reqNo { get; set; }

        [Required]
        [StringLength(12)]
        public string memberCode { get; set; }

        [Required]
        [StringLength(30)]
        public string payOrderNo { get; set; }

        [Column(TypeName = "money")]
        public decimal mgmtFee { get; set; }

        [Column(TypeName = "money")]
        public decimal mgmtPPh { get; set; }

        [Column(TypeName = "money")]
        public decimal mgmtVAT { get; set; }

        public DateTime? mgmtPayOrderDate { get; set; }

        public DateTime? mgmtPaidDate { get; set; }

        public bool isPostFakturPajak { get; set; }

        public DateTime modifTime { get; set; }

        [Required]
        [StringLength(50)]
        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        [Required]
        [StringLength(50)]
        public string inputUN { get; set; }

        public long oracleInvoiceID { get; set; }
    }
}
