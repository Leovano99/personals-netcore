using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    [Table("MS_Detail")]
    public class MS_Detail : AuditedEntity
    {
        public int entityID { get; set; }

        //unique
        [Required]
        [StringLength(5)]
        public string detailCode { get; set; }

        [Required]
        [StringLength(50)]
        public string detailName { get; set; }

        [StringLength(200)]
        public string detailImage { get; set; }

        public int? isMultiple { get; set; }

        public ICollection<MS_Unit> MS_Unit { get; set; }
    }
}
