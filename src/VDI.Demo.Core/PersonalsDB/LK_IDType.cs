using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("LK_IDType")]
    public class LK_IDType : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return idType;
            }
            set { /* nothing */ }
        }

        [Key]
        [StringLength(1)]
        public string idType { get; set; }

        [Required]
        [StringLength(30)]
        public string idTypeName { get; set; }

        [Column("modifTime")]
        public override DateTime? LastModificationTime { get; set; }

        [Column("modifUN")]
        public override long? LastModifierUserId { get; set; }

        [Column("inputTime")]
        public override DateTime CreationTime { get; set; }

        [Column("inputUN")]
        public override long? CreatorUserId { get; set; }

        public virtual ICollection<TR_ID> TR_ID { get; set; }
    }
}
