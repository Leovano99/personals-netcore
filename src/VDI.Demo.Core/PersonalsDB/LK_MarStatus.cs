using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("LK_MarStatus")]
    public class LK_MarStatus : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return marStatus;
            }
            set { /* nothing */ }
        }

        [Key]
        [StringLength(1)]
        public string marStatus { get; set; }

        [Required]
        [StringLength(40)]
        public string marStatusName { get; set; }

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
