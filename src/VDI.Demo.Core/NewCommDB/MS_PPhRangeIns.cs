using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.NewCommDB
{
    [Table("MS_PPhRangeIns")]
    public class MS_PPhRangeIns : Entity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                    "-" + scmCode +
                    "-" + pphRangePct;
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
        public double pphRangePct { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime validDate { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime modifTime { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(40)]
        public string modifUN { get; set; }

        [Key]
        [Column(Order = 6)]
        public DateTime inputTime { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(40)]
        public string inputUN { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(15)]
        public string TAX_CODE { get; set; }
    }
}
