using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("MS_Bank")]
    public class MS_Bank: AuditedEntity
    {
        //unique
        [Required]
        [StringLength(5)]
        public string bankCode { get; set; }

        [Required]
        [StringLength(50)]
        public string bankName { get; set; }

        [Required]
        [StringLength(5)]
        public string parentBankCode { get; set; }

        [Required]
        public bool divertToRO { get; set; }

        [Required]
        [StringLength(200)]
        public string address { get; set; }

        [Required]
        [StringLength(20)]
        public string phone { get; set; }

        [Required]
        [StringLength(20)]
        public string fax { get; set; }

        [Required]
        [StringLength(50)]
        public string headName { get; set; }

        [Required]
        [StringLength(40)]
        public string deputyName1 { get; set; }

        [Required]
        [StringLength(40)]
        public string deputyName2 { get; set; }

        [Required]
        [StringLength(40)]
        public string att { get; set; }

        public bool isActive { get; set; }

        [StringLength(20)]
        public string groupBankCode { get; set; }

        [StringLength(11)]
        public string SWIFTCode { get; set; }

        [StringLength(250)]
        public string relationOfficerEmail { get; set; }

        public int entityID { get; set; }

        public int? bankTermId { get; set; }

        [ForeignKey("LK_BankLevel")]
        public int bankTypeID { get; set; }
        public virtual LK_BankLevel LK_BankLevel { get; set; }


        public ICollection<MS_Account> MS_Account { get; set; }
        public ICollection<MS_BankBranch> MS_BankBranch { get; set; }
    }
}
