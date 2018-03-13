using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("LK_KeyPeople")]
    public class LK_KeyPeople : AuditedEntity
    {
        [Column("keyPeopleId")]
        public override int Id { get; set; }

        [StringLength(100)]
        public string keyPeopleDesc { get; set; }

        [Column("modifTime")]
        public override DateTime? LastModificationTime { get; set; }

        [Column("modifUN")]
        public override long? LastModifierUserId { get; set; }

        [Column("inputTime")]
        public override DateTime CreationTime { get; set; }

        [Column("inputUN")]
        public override long? CreatorUserId { get; set; }

        public virtual ICollection<TR_KeyPeople> TR_KeyPeole { get; set; }
    }
}
