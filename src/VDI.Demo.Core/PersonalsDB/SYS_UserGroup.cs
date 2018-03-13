using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("SYS_UserGroup")]
    public class SYS_UserGroup : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                  "-" + userName +
                  "-" + groupCode;
            }
            set { /* nothing */ }
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string entityCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string userName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string groupCode { get; set; }

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
