using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("MS_Department")]
    public class MS_Department : AuditedEntity
    {
        //unique
        [Required]
        [StringLength(5)]
        public string departmentCode { get; set; }

        [Required]
        [StringLength(50)]
        public string departmentName { get; set; }

        public Boolean isActive { get; set; }

        [StringLength(20)]
        public string departmentWhatsapp { get; set; }
        public string departmentEmail { get; set; }

        public ICollection<MS_Position> MS_Position { get; set; }
        public ICollection<MS_Officer> MS_Officer { get; set; }
    }
}
