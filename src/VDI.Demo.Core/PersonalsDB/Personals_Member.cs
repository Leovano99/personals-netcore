using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("PERSONALS_MEMBER")]
    public class PERSONALS_MEMBER : AuditedEntity<string>
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

        [Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string scmCode { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string memberCode { get; set; }

        [Required]
        [StringLength(20)]
        public string parentMemberCode { get; set; }

        [Required]
        [StringLength(1)]
        public string bankType { get; set; }

        [Required]
        [StringLength(5)]
        public string bankCode { get; set; }

        [Required]
        [StringLength(30)]
        public string bankAccNo { get; set; }

        [Required]
        [StringLength(50)]
        public string bankAccName { get; set; }

        [Required]
        [StringLength(50)]
        public string bankBranchName { get; set; }

        [Required]
        [StringLength(1)]
        public string specCode { get; set; }

        [Required]
        [StringLength(12)]
        public string CDCode { get; set; }

        [Required]
        [StringLength(12)]
        public string ACDCode { get; set; }

        [Required]
        [StringLength(50)]
        public string PTName { get; set; }

        [Required]
        [StringLength(50)]
        public string PrincName { get; set; }

        [Required]
        [StringLength(50)]
        public string mothName { get; set; }

        [Required]
        [StringLength(50)]
        public string spouName { get; set; }

        public DateTime regDate { get; set; }

        public DateTime joinDate { get; set; }

        [Required]
        [StringLength(20)]
        public string userName { get; set; }

        [Required]
        [StringLength(20)]
        public string password { get; set; }

        [Required]
        [StringLength(200)]
        public string remarks1 { get; set; }

        [Required]
        [StringLength(200)]
        public string remarks2 { get; set; }

        [Required]
        [StringLength(200)]
        public string remarks3 { get; set; }

        [Required]
        [StringLength(5)]
        public string memberStatusCode { get; set; }

        public bool isCD { get; set; }

        public bool isACD { get; set; }

        public bool isActive { get; set; }

        public bool isInstitusi { get; set; }

        public bool isPKP { get; set; }

        public bool isMember { get; set; }

        [StringLength(50)]
        public string franchiseGroup { get; set; }

        public bool isActiveEmail { get; set; }

        public int? id_role { get; set; }

        public long? bankAccountRefID { get; set; }

        [Column("modifTime")]
        public override DateTime? LastModificationTime { get; set; }

        [Column("modifUN")]
        public override long? LastModifierUserId { get; set; }

        [Column("inputTime")]
        public override DateTime CreationTime { get; set; }

        [Column("inputUN")]
        public override long? CreatorUserId { get; set; }

        public virtual LK_BankType LK_bankType { get; set; }

        public virtual LK_Spec LK_Spec { get; set; }

        public virtual MS_BankPersonal MS_BankPersonal { get; set; }
    }
}
