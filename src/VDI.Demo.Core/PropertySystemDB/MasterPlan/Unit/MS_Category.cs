using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    [Table("MS_Category")]
    public class MS_Category : AuditedEntity
    {
        //unique
        [Required]
        [StringLength(5)]
        public string categoryCode { get; set; }

        [Required]
        [StringLength(30)]
        public string categoryName { get; set; }

        [Required]
        [StringLength(30)]
        public string projectField { get; set; }

        [Required]
        [StringLength(30)]
        public string areaField { get; set; }

        [Required]
        [StringLength(30)]
        public string categoryField { get; set; }

        [Required]
        [StringLength(30)]
        public string clusterField { get; set; }

        [Required]
        [StringLength(30)]
        public string productField { get; set; }

        [Required]
        [StringLength(30)]
        public string detailField { get; set; }

        [Required]
        [StringLength(30)]
        public string zoningField { get; set; }

        [Required]
        [StringLength(30)]
        public string facingField { get; set; }

        [Required]
        [StringLength(30)]
        public string roadField { get; set; }

        [Required]
        [StringLength(30)]
        public string kavNoField { get; set; }

        public int entityID { get; set; }

        public ICollection<MS_Unit> MS_Unit { get; set; }
        public ICollection<MS_MappingFormula> MS_MappingFormula { get; set; }


    }
}
