using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("MS_JobTitle")]
    public class MS_JobTitle : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return jobTitleID;
            }
            set { /* nothing */ }
        }

        [Key]
        [StringLength(3)]
        public string jobTitleID { get; set; }

        [Required]
        [StringLength(50)]
        public string jobTitleName { get; set; }

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
