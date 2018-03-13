using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.AccountingDB
{
    [Table("TR_PaymentDetailJournal")]
    public class TR_PaymentDetailJournal : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                  "-" + accCode +
                  "-" + transNo +
                  "-" + payNo +
                  "-" + bookCode;
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
        [StringLength(20)]
        public string transNo { get; set; }

        [Key]
        [Column(Order = 3)]
        public int payNo { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(20)]
        public string bookCode { get; set; }

        [Required]
        [StringLength(30)]
        public string journalCode { get; set; }
    }
}
