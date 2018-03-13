using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.NewCommDB
{
    [Table("MS_CommPct")]
    public class MS_CommPct : Entity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                    "-" + scmCode +
                    "-" + statusCode;
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
        public string statusCode { get; set; }

        [Required]
        [StringLength(3)]
        public string commTypeCode { get; set; }

        [Key]
        [Column(Order = 3)]
        public byte asUplineNo { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime validDate { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "money")]
        public decimal minAmt { get; set; }

        [Column(TypeName = "money")]
        public decimal maxAmt { get; set; }

        public double commPctPaid { get; set; }

        public double commPctHold { get; set; }

        public DateTime modifTime { get; set; }

        [Required]
        [StringLength(40)]
        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        [Required]
        [StringLength(40)]
        public string inputUN { get; set; }
    }
}
