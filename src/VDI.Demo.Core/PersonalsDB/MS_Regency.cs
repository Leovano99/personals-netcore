using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("MS_Regency")]
    public class MS_Regency : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return regencyCode +
                  "-" + regencyName;
            }
            set { /* nothing */ }
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string regencyCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string regencyName { get; set; }

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
