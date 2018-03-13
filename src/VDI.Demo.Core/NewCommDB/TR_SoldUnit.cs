using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.NewCommDB
{
    [Table("TR_SoldUnit")]
    public class TR_SoldUnit : Entity<string>
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
        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string entityCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string scmCode { get; set; }

        [Required]
        [StringLength(5)]
        public string propCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string devCode { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string bookNo { get; set; }

        [Required]
        [StringLength(11)]
        public string batchNo { get; set; }

        [Required]
        [StringLength(12)]
        public string memberCode { get; set; }

        [Required]
        [StringLength(12)]
        public string CDCode { get; set; }

        [Required]
        [StringLength(12)]
        public string ACDCode { get; set; }

        [Required]
        [StringLength(20)]
        public string roadCode { get; set; }

        [Required]
        [StringLength(50)]
        public string roadName { get; set; }

        [Required]
        [StringLength(10)]
        public string unitNo { get; set; }

        public DateTime bookDate { get; set; }

        public float unitLandArea { get; set; }

        public float unitBuildArea { get; set; }

        [Column(TypeName = "money")]
        public decimal netNetPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal unitPrice { get; set; }

        public double pctComm { get; set; }

        public double pctBobot { get; set; }

        public DateTime? PPJBDate { get; set; }

        public DateTime? xreqInstPayDate { get; set; }

        public DateTime? xprocessDate { get; set; }

        public DateTime? cancelDate { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }

        public DateTime? holdDate { get; set; }

        public DateTime modifTime { get; set; }

        [Required]
        [StringLength(40)]
        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        [Required]
        [StringLength(40)]
        public string inputUN { get; set; }

        public bool calculateUseMaster { get; set; }
    }
}
