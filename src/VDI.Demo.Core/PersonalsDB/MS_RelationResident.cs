using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("MS_RelationResident")]
    public class MS_RelationResident : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return kkCode +
                  "-" + RefID;
            }
            set { /* nothing */ }
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string kkCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string RefID { get; set; }

        [StringLength(10)]
        public string psCode { get; set; }

        [StringLength(100)]
        public string remark { get; set; }

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
