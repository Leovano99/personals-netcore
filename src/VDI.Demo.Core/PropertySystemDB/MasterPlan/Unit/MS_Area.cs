using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    [Table("MS_Area")]
    public class MS_Area : AuditedEntity
    {
        public int entityID { get; set; }

        //unique
        [Required]
        [StringLength(5)]
        public string areaCode { get; set; }

        [Required]
        [StringLength(30)]
        public string regionName { get; set; }

        [ForeignKey("MS_City")]
        public int cityID { get; set; }
        public virtual MS_City MS_City { get; set; }

        public ICollection<MS_Unit> MS_Unit { get; set; }

    }
}
