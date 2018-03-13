using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    [Table("MS_County")]
    public class MS_County : AuditedEntity
    {
        [Required]
        public string countyName { get; set; }

        [ForeignKey("MS_Territory")]
        public int territoryID { get; set; }
        public virtual MS_Territory MS_Territory { get; set; }

        public ICollection<MS_City> MS_City { get; set; }
    }
}
