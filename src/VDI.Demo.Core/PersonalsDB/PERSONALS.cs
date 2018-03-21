using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Abp.Domain.Entities.Auditing;

namespace VDI.Demo.PersonalsDB
{
    [Table("PERSONALS")]
    public partial class PERSONALS : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                  "-" + psCode;
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
        
        [StringLength(8)]
        public string parentPSCode { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(1)]
        public string sex { get; set; }

        public DateTime? birthDate { get; set; }
        
        [StringLength(30)]
        public string birthPlace { get; set; }

        [Required]
        [StringLength(1)]
        public string marCode { get; set; }

        [Required]
        [StringLength(1)]
        public string relCode { get; set; }

        [Required]
        [StringLength(1)]
        public string bloodCode { get; set; }

        [StringLength(3)]
        public string occID { get; set; }

        [Required]
        [StringLength(3)]
        public string nationID { get; set; }

        [Required]
        [StringLength(1)]
        public string familyStatus { get; set; }
        
        [StringLength(30)]
        public string NPWP { get; set; }

        [Required]
        [StringLength(2)]
        public string FPTransCode { get; set; }

        [Required]
        [StringLength(1)]
        public string grade { get; set; }

        public bool isActive { get; set; }

        [StringLength(500)]
        public string remarks { get; set; }
        
        [StringLength(10)]
        public string mailGroup { get; set; }

        public bool isInstitute { get; set; }

        [StringLength(50)]
        public string UploadContentType { get; set; }

        public string UploadContentImage { get; set; }

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
