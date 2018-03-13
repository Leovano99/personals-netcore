using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("MS_BankBranch")]
    public class MS_BankBranch : AuditedEntity
    {
        public int entityID { get; set; }

        //unique
        [Required]
        [StringLength(5)]
        public string bankBranchCode { get; set; }

        [Required]
        [StringLength(20)]
        public string bankRekNo { get; set; }

        [Required]
        [StringLength(50)]
        public string bankBranchName { get; set; }

        [StringLength(50)]
        public string PICName { get; set; }

        [StringLength(50)]
        public string PICPosition { get; set; }

        [StringLength(20)]
        public string phone { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        public bool isActive { get; set; }

        [Required]
        [StringLength(5)]
        public string projectCode { get; set; }

        [StringLength(5)]
        public string clusterCode { get; set; }

        public string bankBranchType { get; set; }

        [ForeignKey("MS_Bank")]
        public int bankID { get; set; }
        public virtual MS_Bank MS_Bank { get; set; }

        [ForeignKey("LK_BankLevel")]
        public int bankBranchTypeID { get; set; }
        public virtual LK_BankLevel LK_BankLevel { get; set; }

        public ICollection<MP_BankBranch_JKB> MP_BankBranch_JKB { get; set; }
        public ICollection<MS_Account> MS_Account { get; set; }

    }
}
