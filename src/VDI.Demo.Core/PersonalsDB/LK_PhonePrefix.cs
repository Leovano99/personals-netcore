using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("LK_PhonePrefix")]
    public class LK_PhonePrefix : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return prefix +
                  "-" + phoneType;
            }
            set { /* nothing */ }
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string prefix { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string phoneType { get; set; }

        public int length { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

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
