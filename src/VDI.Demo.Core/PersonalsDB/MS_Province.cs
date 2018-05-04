using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("MS_Province")]
    public class MS_Province : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return provinceCode;
            }
            set { /* nothing */ }
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string provinceCode { get; set; }
        
        [Column(Order = 1)]
        [StringLength(50)]
        public string provinceName { get; set; }

        [StringLength(3)]
        public string ppatkProvinceCode { get; set; }

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
