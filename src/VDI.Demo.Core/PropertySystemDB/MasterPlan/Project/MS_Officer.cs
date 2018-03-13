using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("MS_Officer")]
    public class MS_Officer : AuditedEntity
    {
        [Required]
        [StringLength(25)]
        public string officerName { get; set; }

        [Required]
        [StringLength(50)]
        public string officerEmail { get; set; }

        [Required]
        [StringLength(20)]
        public string officerPhone { get; set; }

        public Boolean isActive { get; set; }

        [ForeignKey("MS_Position")]
        public int positionID { get; set; }
        public virtual MS_Position MS_Position { get; set; }

        [ForeignKey("MS_Department")]
        public int departmentID { get; set; }
        public virtual MS_Department MS_Department { get; set; }

        public ICollection<MP_OfficerProject> MP_OfficerProject { get; set; }
    }
}
