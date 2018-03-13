using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.AccountingDB
{
    [Table("TR_Journal")]
    public class TR_Journal : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                  "-" + journalCode +
                  "-" + COACodeFIN +
                  "-" + COACodeAcc;
            }
            set { /* nothing */ }
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string entityCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string journalCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string COACodeFIN { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(5)]
        public string COACodeAcc { get; set; }
        
        public DateTime journalDate { get; set; }
        
        [Column(TypeName = "money")]
        public decimal debit { get; set; }
        
        [Column(TypeName = "money")]
        public decimal kredit { get; set; }
        
        [StringLength(50)]
        public string remarks { get; set; }
    }
}
