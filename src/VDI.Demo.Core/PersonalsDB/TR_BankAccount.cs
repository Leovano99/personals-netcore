using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("TR_BankAccount")]
    public class TR_BankAccount : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                  "-" + psCode +
                  "-" + refID +
                  "-" + BankCode;
            }
            set { /* nothing */ }
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string entityCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string psCode { get; set; }

        [Key]
        [Column(Order = 2)]
        public int refID { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(5)]
        public string BankCode { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountNo { get; set; }

        [StringLength(200)]
        public string AccountName { get; set; }
        
        [StringLength(50)]
        public string BankBranchName { get; set; }
        
        public bool isAutoDebit { get; set; } = false;

        public bool isMain { get; set; } = false;

        [Column("modifTime")]
        public override DateTime? LastModificationTime { get; set; }

        [Column("modifUN")]
        public override long? LastModifierUserId { get; set; }

        [Column("inputTime")]
        public override DateTime CreationTime { get; set; }

        [Column("inputUN")]
        public override long? CreatorUserId { get; set; }
    }
}
