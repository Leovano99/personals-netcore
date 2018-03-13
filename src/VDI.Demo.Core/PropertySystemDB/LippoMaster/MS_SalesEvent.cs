using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("MS_SalesEvent")]
    public class MS_SalesEvent : AuditedEntity
    {
        public int EntityID { get; set; }

        [Required]
        [StringLength(5)]
        public string eventCode { get; set; }

        [Required]
        [StringLength(50)]
        public string eventName { get; set; }
        
        public int sortNo { get; set; }

        public virtual ICollection<TR_BookingHeader> TR_BookingHeader { get; set; }
    }
}
