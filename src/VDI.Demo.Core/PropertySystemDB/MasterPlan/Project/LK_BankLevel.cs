using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("LK_BankLevel")]
    public class LK_BankLevel : AuditedEntity
    {
        [Required]
        [StringLength(3)]
        public string bankLevelCode { get; set; }

        [Required]
        [StringLength(20)]
        public string bankLevelName { get; set; }

        public bool isBankType { get; set; }

        public bool isBankBranchType { get; set; }

        public ICollection<MS_Bank> MS_Bank { get; set; }
        public ICollection<MS_BankBranch> MS_BankBranch { get; set; }
    }
}
