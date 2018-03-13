using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.NewCommDB
{
    [Table("MS_PPhRange")]
    public class MS_PPhRange : Entity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                    "-" + scmCode +
                    "-" + PPhYear;
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
        public int PPhYear { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal PPhRangeHighBound { get; set; }

        public double PPhRangePct { get; set; }

        public DateTime modifTime { get; set; }

        [Required]
        [StringLength(40)]
        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        [Required]
        [StringLength(40)]
        public string inputUN { get; set; }

        [Required]
        [StringLength(15)]
        public string TAX_CODE { get; set; }

        [Required]
        [StringLength(15)]
        public string TAX_CODE_NON_NPWP { get; set; }

        [Required]
        [StringLength(15)]
        public string pphRangePct_NON_NPWP { get; set; }
    }
}
