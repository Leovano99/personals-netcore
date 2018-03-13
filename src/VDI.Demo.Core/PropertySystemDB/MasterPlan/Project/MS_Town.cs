using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("MS_Town")]
    public class MS_Town : AuditedEntity
    {
        //unique
        [Required]
        [StringLength(5)]
        public string townCode { get; set; }

        [Required]
        [StringLength(100)]
        public string townName { get; set; }

        [ForeignKey("MS_Country")]
        public int countryID { get; set; }
        public virtual MS_Country MS_Country { get; set; }

        public ICollection<MS_PostCode> MS_PostCode { get; set; }
    }
}
