using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("MP_BankBranch_JKB")]
    public class MP_BankBranch_JKB : AuditedEntity
    {
        [ForeignKey("MS_BankBranch")]
        public int bankBranchID { get; set; }
        public virtual MS_BankBranch MS_BankBranch { get; set; }

        [ForeignKey("MS_JenisKantorBank")]
        public int JKBID { get; set; }
        public virtual MS_JenisKantorBank MS_JenisKantorBank { get; set; }

    }
}
