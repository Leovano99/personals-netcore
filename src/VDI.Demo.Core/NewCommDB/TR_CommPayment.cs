using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.NewCommDB
{
    [Table("TR_CommPayment")]
    public class TR_CommPayment : Entity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                    "-" + scmCode +
                    "-" + devCode;
            }
            set { /* nothing */ }
        }
        [Required]
        [StringLength(1)]
        public string entityCode { get; set; }

        [Required]
        [StringLength(3)]
        public string scmCode { get; set; }

        [Required]
        [StringLength(5)]
        public string propCode { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string devCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string bookNo { get; set; }

        [Key]
        [Column(Order = 2)]
        public short asUplineNo { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(1)]
        public string isHold { get; set; }

        [Key]
        [Column(Order = 4)]
        public short commNo { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(12)]
        public string memberCode { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(3)]
        public string commTypeCode { get; set; }

        [Key]
        [Column(Order = 7)]
        public byte reqNo { get; set; }

        [Required]
        [StringLength(30)]
        public string commPayCode { get; set; }

        [Required]
        [StringLength(30)]
        public string payOrderNo { get; set; }

        public DateTime schedDate { get; set; }

        [Column(TypeName = "money")]
        public decimal amount { get; set; }

        [Required]
        [StringLength(100)]
        public string desc { get; set; }

        public DateTime? pphProcessDate { get; set; }

        public short? pphYear { get; set; }

        [Column(TypeName = "money")]
        public decimal? pphAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal PPNAmount { get; set; }

        public bool isAutoCalc { get; set; }

        [Required]
        [StringLength(5)]
        public string bankCode { get; set; }

        [Required]
        [StringLength(1)]
        public string bankType { get; set; }

        [Required]
        [StringLength(50)]
        public string bankAccNo { get; set; }

        [Required]
        [StringLength(50)]
        public string bankAccName { get; set; }

        [Required]
        [StringLength(50)]
        public string bankBranchName { get; set; }

        public DateTime? payOrderDate { get; set; }

        public DateTime? paidDate { get; set; }

        [StringLength(32)]
        public string paidNo { get; set; }

        [Column(TypeName = "money")]
        public decimal pointValue { get; set; }

        public DateTime modifTime { get; set; }

        [Required]
        [StringLength(40)]
        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        [Required]
        [StringLength(40)]
        public string inputUN { get; set; }

        [Required]
        [StringLength(50)]
        public string memberName { get; set; }

        [Required]
        [StringLength(30)]
        public string NPWP { get; set; }

        public bool isInstitusi { get; set; }

        public long oracleInvoiceID { get; set; }
    }
}
