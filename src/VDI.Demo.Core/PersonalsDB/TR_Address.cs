using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("TR_Address")]
    public class TR_Address : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                  "-" + psCode +
                  "-" + refID +
                  "-" + addrType;
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

        [Key]
        [Column(Order = 3)]
        [StringLength(1)]
        public string addrType { get; set; }

        [Required]
        [StringLength(500)]
        public string address { get; set; }

        [StringLength(10)]
        public string postCode { get; set; }

        [Required]
        [StringLength(50)]
        public string city { get; set; }

        [Required]
        [StringLength(50)]
        public string country { get; set; }

        [StringLength(250)]
        public string Kelurahan { get; set; }

        [StringLength(250)]
        public string Kecamatan { get; set; }

        [StringLength(250)]
        public string province { get; set; }

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
