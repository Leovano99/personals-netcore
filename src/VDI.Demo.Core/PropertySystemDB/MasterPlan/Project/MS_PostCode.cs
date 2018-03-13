using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("MS_PostCode")]
    public class MS_PostCode : AuditedEntity
    {
        //unique
        [Required]
        [StringLength(5)]
        public string postCode { get; set; }

        [Required]
        [StringLength(100)]
        public string province { get; set; }

        [Required]
        [StringLength(100)]
        public string regency { get; set; }

        [Required]
        [StringLength(100)]
        public string district { get; set; }

        [Required]
        [StringLength(100)]
        public string village { get; set; }

        [ForeignKey("MS_Town")]
        public int townID { get; set; }
        public virtual MS_Town MS_Town { get; set; }
    }
}
