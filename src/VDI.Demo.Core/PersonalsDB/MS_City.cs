using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("MS_City")]
    public class MS_City : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                  "-" + cityCode;
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

        [Required]
        [StringLength(50)]
        public string cityName { get; set; }

        [Required]
        [StringLength(100)]
        public string country { get; set; }

        [StringLength(5)]
        public string provinceCode { get; set; }

        [StringLength(10)]
        public string cityAbbreviation { get; set; }

        [Column("modifTime")]
        public override DateTime? LastModificationTime { get; set; }

        [Column("modifUN")]
        public override long? LastModifierUserId { get; set; }

        [Column("inputTime")]
        public override DateTime CreationTime { get; set; }

        [Column("inputUN")]
        public override long? CreatorUserId { get; set; }

        public virtual LK_Country LK_Country { get; set; }
        
        [StringLength(5)]
        public string countyCode { get; set; }

        //public virtual MS_County MS_County { get; set; }

        //public virtual MS_Province MS_Province { get; set; }

        //public virtual ICollection<MS_PostCode> MS_PostCode { get; set; }
    }
}
