using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    [Table("MS_Product")]
    public class MS_Product : AuditedEntity
    {
        public int entityID { get; set; }

        //unique
        [Required]
        [StringLength(5)]
        public string productCode { get; set; }

        [Required]
        [StringLength(30)]
        public string productName { get; set; }

        [Required]
        public int sortNo { get; set; }

        public ICollection<MS_Unit> MS_Unit { get; set; }
        public ICollection<MS_MappingFormula> MS_MappingFormula { get; set; }
        public ICollection<MS_ProjectProduct> MS_ProjectProduct { get; set; }
    }
}
