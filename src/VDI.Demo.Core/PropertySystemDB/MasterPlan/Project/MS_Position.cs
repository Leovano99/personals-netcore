using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("MS_Position")]
    public class MS_Position : AuditedEntity
    {
        [Required]
        [StringLength(25)]
        public string positionName { get; set; }

        //unique
        [Required]
        [StringLength(5)]
        public string positionCode { get; set; }

        public Boolean isActive { get; set; }

        [ForeignKey("MS_Department")]
        public int departmentID { get; set; }
        public virtual MS_Department MS_Department { get; set; }

        public ICollection<MS_Officer> MS_Officer { get; set; }

    }
}
