using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("MS_PostCode")]
    public partial class MS_PostCode : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                  "-" + cityCode +
                  "-" + postCode;
            }
            set { /* nothing */ }
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string entityCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(4)]
        public string cityCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string postCode { get; set; }

        [Column("modifTime")]
        public override DateTime? LastModificationTime { get; set; }

        [Column("modifUN")]
        public override long? LastModifierUserId { get; set; }

        [Column("inputTime")]
        public override DateTime CreationTime { get; set; }

        [Column("inputUN")]
        public override long? CreatorUserId { get; set; }

        public virtual MS_City MS_City { get; set; }

        public virtual ICollection<MS_Street> MS_Street { get; set; }
    }
}
