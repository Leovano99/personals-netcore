using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    [Table("MS_Renovation")]
    public class MS_Renovation : AuditedEntity
    {
        //unique
        [Required]
        [StringLength(5)]
        public string renovationCode { get; set; }

        [Required]
        [StringLength(100)]
        public string renovationName { get; set; }

        [StringLength(300)]
        public string detail { get; set; }

        [ForeignKey("MS_Project")]
        public int projectID { get; set; }
        public virtual MS_Project MS_Project { get; set; }

        public bool isActive { get; set; }

        public ICollection<MS_UnitItemPrice> MS_UnitItemPrice { get; set; }

        public ICollection<TR_UnitOrderDetail> TR_UnitOrderDetail { get; set; }

        public ICollection<TR_UnitReserved> TR_UnitReserved { get; set; }



    }
}
