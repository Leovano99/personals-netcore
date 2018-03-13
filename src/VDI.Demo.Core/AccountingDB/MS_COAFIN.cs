using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.AccountingDB
{
    [Table("MS_COAFIN")]
    public class MS_COAFIN : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
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
        [StringLength(10)]
        public string COACodeFIN { get; set; }

        [Required]
        [StringLength(50)]
        public string COACodeName { get; set; }
    }
}
