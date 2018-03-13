using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.NewCommDB
{
    [Table("MS_GroupSchemaRequirement")]
    public class MS_GroupSchemaRequirement : AuditedEntity
    {
        public Byte reqNo { get; set; }

        [Required]
        [StringLength(40)]
        public string reqDesc { get; set; }

        [Required]
        public double pctPaid { get; set; }

        [Required]
        public double orPctPaid { get; set; }

        [ForeignKey("MS_GroupSchema")]
        public int groupSchemaID { get; set; }
        public virtual MS_GroupSchema MS_GroupSchema { get; set; }

        public int entityID { get; set; }

        public bool isComplete { get; set; }
    }
}
