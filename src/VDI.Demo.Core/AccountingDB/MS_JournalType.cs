using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.AccountingDB
{
    [Table("MS_JournalType")]
    public class MS_JournalType : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                  "-" + journalTypeCode +
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
        [StringLength(11)]
        public string journalTypeCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string COACodeFIN { get; set; }

        [Required]
        [StringLength(1)]
        public string amtTypeCode { get; set; }

        public short ACCAlloc { get; set; }
    }
}
