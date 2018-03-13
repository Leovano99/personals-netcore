using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("LK_Religion")]
    public class LK_Religion : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return relCode;
            }
            set { /* nothing */ }
        }

        [Key]
        [StringLength(1)]
        public string relCode { get; set; }

        [Required]
        [StringLength(20)]
        public string relName { get; set; }

        [StringLength(20)]
        public string relNameProInt { get; set; }

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
