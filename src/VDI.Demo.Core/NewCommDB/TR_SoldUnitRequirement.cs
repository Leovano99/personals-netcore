using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.NewCommDB
{
    [Table("TR_SoldUnitRequirement")]
    public class TR_SoldUnitRequirement : Entity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                    "-" + devCode +
                    "-" + bookNo;
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
        public string devCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string bookNo { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(3)]
        public string scmCode { get; set; }

        [Key]
        [Column(Order = 4)]
        public byte reqNo { get; set; }

        [Required]
        [StringLength(40)]
        public string reqDesc { get; set; }

        public double pctPaid { get; set; }

        public double? orPctPaid { get; set; }

        public DateTime? reqDate { get; set; }

        public DateTime? processDate { get; set; }

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
