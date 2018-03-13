using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.LippoMaster;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("MS_Account")]
    public class MS_Account : AuditedEntity
    {
        public int entityID { get; set; }

        //unique
        [StringLength(5)]
        public string accCode { get; set; }

        [Required]
        [StringLength(5)]
        public string devCode { get; set; }

        [Required]
        [StringLength(60)]
        public string accName { get; set; }

        [Required]
        [StringLength(40)]
        public string accNo { get; set; }

        public bool isActive { get; set; }

        [StringLength(10)]
        public string NATURE_ACCOUNT_BANK { get; set; }

        [StringLength(10)]
        public string NATURE_ACCOUNT_DEP { get; set; }

        [StringLength(5)]
        public string ORG_ID { get; set; }

        [StringLength(2)]
        public string PROVINCE_ID { get; set; }

        [ForeignKey("MS_Bank")]
        public int bankID { get; set; }
        public virtual MS_Bank MS_Bank { get; set; }

        [ForeignKey("MS_BankBranch")]
        public int bankBranchID { get; set; }
        public virtual MS_BankBranch MS_BankBranch { get; set; }

        [ForeignKey("MS_Company")]
        public int coID { get; set; }
        public virtual MS_Company MS_Company { get; set; }

        [ForeignKey("MS_Project")]
        public int projectID { get; set; }
        public virtual MS_Project MS_Project { get; set; }

        public virtual ICollection<TR_SSPDetail> TR_SSPDetail { get; set; }

        public ICollection<TR_PaymentHeader> TR_PaymentHeader { get; set; }
        public ICollection<TR_PaymentDetail> TR_PaymentDetail { get; set; }
        public ICollection<TR_PaymentDetailAlloc> TR_PaymentDetailAlloc { get; set; }

        public ICollection<SYS_FinanceCounter> SYS_FinanceCounter { get; set; }
        public ICollection<MS_AccountEmail> MS_AccountEmail { get; set; }
    }
}
