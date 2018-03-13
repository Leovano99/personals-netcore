using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    [Table("MS_City")]
    public class MS_City : AuditedEntity
    {
        [Required]
        public string cityName { get; set; }

        [ForeignKey("MS_County")]
        public int countyID { get; set; }
        public virtual MS_County MS_County { get; set; }

        public ICollection<MS_Area> MS_Area { get; set; }
    }
}
