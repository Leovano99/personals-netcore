using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("MS_Country")]
    public class MS_Country : AuditedEntity
    {
        //unique
        [Required]
        [StringLength(5)]
        public string countryCode { get; set; }

        [Required]
        [StringLength(100)]
        public string countryName { get; set; }

        public ICollection<MS_Town> MS_Town { get; set; }
    }
}
