using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("MS_Document")]
    public class MS_Document : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return documentType;
            }
            set { /* nothing */ }
        }

        [Key]
        [StringLength(25)]
        public string documentType { get; set; }

        [StringLength(50)]
        public string documentName { get; set; }

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
