using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("MS_Entity")]
    public class MS_Entity : AuditedEntity
    {
        //unique
        [Required]
        [StringLength(1)]
        public string entityCode { get; set; }

        [Required]
        [StringLength(40)]
        public string entityName { get; set; }
    }
}
