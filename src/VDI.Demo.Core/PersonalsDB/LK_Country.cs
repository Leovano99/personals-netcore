using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("LK_Country")]
    public class LK_Country : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return country;
            }
            set { /* nothing */ }
        }

        [Key]
        [StringLength(100)]
        public string country { get; set; }

        public int urut { get; set; }

        [StringLength(3)]
        public string ppatkCountryCode { get; set; }

        [Column("modifTime")]
        public override DateTime? LastModificationTime { get; set; }

        [Column("modifUN")]
        public override long? LastModifierUserId { get; set; }

        [Column("inputTime")]
        public override DateTime CreationTime { get; set; }

        [Column("inputUN")]
        public override long? CreatorUserId { get; set; }

        public virtual ICollection<MS_City> MS_City { get; set; }
    }
}
