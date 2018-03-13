using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VDI.Demo.PropertySystemDB.LippoMaster;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    public class MS_Facade : AuditedEntity
    {
        public int entityID { get; set; }

        //unique
        [Required]
        [StringLength(5)]
        public string facadeCode { get; set; }

        [Required]
        [StringLength(50)]
        public string facadeName { get; set; }

        public virtual ICollection<TR_BookingHeader> TR_BookingHeader { get; set; }
    }
}
