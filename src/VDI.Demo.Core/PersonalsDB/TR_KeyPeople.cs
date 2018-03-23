using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("TR_KeyPeople")]
    public class TR_KeyPeople : AuditedEntity
    {
        [Column("trKeyPeopleId")]
        public override int Id { get; set; }

        [StringLength(8)]
        public string psCode { get; set; }

        public int refID { get; set; }

        [ForeignKey("LK_KeyPeople")]
        public int keyPeopleId { get; set; }
        public virtual LK_KeyPeople LK_KeyPeople { get; set; }

        [StringLength(100)]
        public string keyPeopleName { get; set; }

        [StringLength(8)]
        public string keyPeoplePSCode { get; set; }

        public bool isActive { get; set; }

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
