using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.NewCommDB
{
    [Table("TR_BudgetPayment")]
    public class TR_BudgetPayment : Entity<string>
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
        public byte reqNo { get; set; }

        [Required]
        [StringLength(12)]
        public string memberCode { get; set; }

        [Required]
        [StringLength(30)]
        public string payOrderNo { get; set; }

        [Column(TypeName = "money")]
        public decimal budgetAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal budgetPPh { get; set; }

        [Column(TypeName = "money")]
        public decimal budgetVAT { get; set; }

        public DateTime? budgetPayOrderDate { get; set; }

        public DateTime? budgetPaidDate { get; set; }

        public bool isPostFakturPajak { get; set; }

        public DateTime modifTime { get; set; }

        [Required]
        [StringLength(40)]
        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        [Required]
        [StringLength(40)]
        public string inputUN { get; set; }

        public long oracleInvoiceID { get; set; }
    }
}
