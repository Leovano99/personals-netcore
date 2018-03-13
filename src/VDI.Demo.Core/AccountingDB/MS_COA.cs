using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.AccountingDB
{
    [Table("MS_COA")]
    public class MS_COA : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                  "-" + accCode +
                  "-" + COACodeFIN;
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
        public string accCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string COACodeFIN { get; set; }

        [Required]
        [StringLength(5)]
        public string COACodeAcc { get; set; }

        [Required]
        [StringLength(50)]
        public string COAName { get; set; }

        [Required]
        [StringLength(50)]
        public string remarks { get; set; }
    }
}
