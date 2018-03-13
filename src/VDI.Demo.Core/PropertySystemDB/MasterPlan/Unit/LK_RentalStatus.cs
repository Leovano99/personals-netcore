using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    [Table("LK_RentalStatus")]
    public class LK_RentalStatus : AuditedEntity
    {
        //unique
        [Required]
        [StringLength(1)]
        public string rentalStatusCode { get; set; }

        [Required]
        [StringLength(50)]
        public string rentalStatusName { get; set; }

        public ICollection<MS_Unit> MS_Unit { get; set; }
    }
}
