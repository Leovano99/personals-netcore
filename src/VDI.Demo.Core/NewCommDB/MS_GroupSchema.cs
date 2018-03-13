using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.NewCommDB
{
    [Table("MS_GroupSchema")]
    public class MS_GroupSchema : AuditedEntity
    {
        [Required]
        [StringLength(4)]
        public string groupSchemaCode { get; set; }

        [Required]
        [StringLength(50)]
        public string groupSchemaName { get; set; }

        public bool isStandard { get; set; }

        public DateTime validFrom { get; set; }

        public string documentGrouping { get; set; }

        public int projectID { get; set; }

        public int clusterID { get; set; }

        [ForeignKey("MS_Schema")]
        public int schemaID { get; set; }
        public virtual MS_Schema MS_Schema { get; set; }

        public int entityID { get; set; }

        public bool isActive { get; set; }

        public bool isComplete { get; set; }

        public ICollection<MS_GroupCommPct> MS_GroupCommPct { get; set; }
        public ICollection<MS_GroupCommPctNonStd> MS_GroupCommPctNonStd { get; set; }
        public ICollection<MS_GroupSchemaRequirement> MS_GroupSchemaRequirement { get; set; }
    }
}
