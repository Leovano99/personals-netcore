using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.TAXDB
{
    [Table("msBatchPajakStock")]
    public class msBatchPajakStock : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return BatchID +
                  "-" + CoCode +
                  "-" + FPBranchCode +
                  "-" + FPYear +
                  "-" + FPNo;
            }
            set { /* nothing */ }
        }

        [Key]
        [Column(Order = 0)]
        public int BatchID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string CoCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string FPBranchCode { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(2)]
        public string FPYear { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(8)]
        public string FPNo { get; set; }

        public bool? IsAvailable { get; set; }

        [StringLength(50)]
        public string YearPeriod { get; set; }

        [StringLength(3)]
        public string FPTransCode { get; set; }

        [StringLength(3)]
        public string FPStatCode { get; set; }
    }
}
