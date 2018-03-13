using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("MS_PriorityPass")]
    public class MS_PriorityPass : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return PPNo;
            }
            set { /* nothing */ }
        }

        [Key]
        [StringLength(10)]
        public string PPNo { get; set; }

        [Required]
        [StringLength(50)]
        public string custName { get; set; }

        public int queueNo { get; set; }

        [Required]
        [StringLength(1)]
        public string statusCode { get; set; }

        [Required]
        [StringLength(50)]
        public string dealCloserID { get; set; }

        [Required]
        [StringLength(100)]
        public string dealCloserName { get; set; }

        [Required]
        [StringLength(100)]
        public string scmName { get; set; }

        [Required]
        [StringLength(100)]
        public string dealCloserPhone { get; set; }

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
