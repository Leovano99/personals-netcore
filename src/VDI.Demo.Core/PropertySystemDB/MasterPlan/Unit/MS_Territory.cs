using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    [Table("MS_Territory")]
    public class MS_Territory : AuditedEntity
    {
        [Required]
        public string territoryName { get; set; }

        public ICollection<MS_County> MS_County { get; set; }
    }
}
