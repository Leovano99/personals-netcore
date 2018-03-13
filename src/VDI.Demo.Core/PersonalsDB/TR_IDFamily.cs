using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("TR_IDFamily")]
    public class TR_IDFamily : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return psCode +
                  "-" + familyRefID +
                  "-" + refID;
            }
            set { /* nothing */ }
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string psCode { get; set; }

        [Key]
        [Column(Order = 1)]
        public int familyRefID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int refID { get; set; }

        [Required]
        [StringLength(1)]
        public string idType { get; set; }

        [Required]
        [StringLength(50)]
        public string idNo { get; set; }

        public DateTime? expiredDate { get; set; }

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
