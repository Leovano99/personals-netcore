using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("MS_Village")]
    public class MS_Village : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return villageCode +
                  "-" + villageName +
                  "-" + cityCode +
                  "-" + countyCode +
                  "-" + regencyCode +
                  "-" + provinceCode
                    ;
            }
            set { /* nothing */ }
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string villageCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string villageName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string cityCode { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(5)]
        public string countyCode { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(5)]
        public string regencyCode { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(5)]
        public string provinceCode { get; set; }

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
