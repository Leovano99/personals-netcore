using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.NewCommDB
{
    [Table("MS_ManagementPct")]
    public class MS_ManagementPct : AuditedEntity
    {
        public double managementPct { get; set; }

        [Required]
        [StringLength(5)]
        public string bankCode { get; set; }

        [Required]
        [StringLength(50)]
        public string bankAccountName { get; set; }

        [Required]
        [StringLength(50)]
        public string bankBranchName { get; set; }

        [ForeignKey("MS_Schema")]
        public int schemaID { get; set; }
        public virtual MS_Schema MS_Schema { get; set; }

        [ForeignKey("MS_Developer_Schema")]
        public int developerSchemaID { get; set; }
        public virtual MS_Developer_Schema MS_Developer_Schema { get; set; }

        public int entityID { get; set; }

        public bool isActive { get; set; }

        public bool isComplete { get; set; }

    }
}
