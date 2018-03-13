using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.AccountingDB
{
    [Table("MS_Mapping")]
    public class MS_Mapping : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                  "-" + payForCode +
                  "-" + payTypeCode +
                  "-" + othersTypeCode +
                  "-" + journalTypeCode;
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
        public string payForCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string payTypeCode { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(3)]
        public string othersTypeCode { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(11)]
        public string journalTypeCode { get; set; }
    }
}
