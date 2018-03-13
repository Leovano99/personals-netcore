using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("TR_Company")]
    public class TR_Company : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                  "-" + psCode +
                  "-" + refID;
            }
            set { /* nothing */ }
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string entityCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string psCode { get; set; }

        [Key]
        [Column(Order = 2)]
        public int refID { get; set; }

        [Required]
        [StringLength(200)]
        public string coName { get; set; }

        [Required]
        [StringLength(500)]
        public string coAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string coCity { get; set; }

        [Required]
        [StringLength(10)]
        public string coPostCode { get; set; }

        [Required]
        [StringLength(50)]
        public string coCountry { get; set; }

        [Required]
        [StringLength(100)]
        public string coType { get; set; }

        [Required]
        [StringLength(100)]
        public string jobTitle { get; set; }

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
