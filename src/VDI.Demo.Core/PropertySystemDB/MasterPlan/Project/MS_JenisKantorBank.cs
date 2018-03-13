using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("MS_JenisKantorBank")]
    public class MS_JenisKantorBank : AuditedEntity
    {

        [Required]
        [StringLength(100)]
        public string JKBName { get; set; }

        //unique
        [Required]
        [StringLength(5)]
        public string JKBCode { get; set; }

        public ICollection<MP_BankBranch_JKB> MP_BankBranch_JKB { get; set; }
    }
}
