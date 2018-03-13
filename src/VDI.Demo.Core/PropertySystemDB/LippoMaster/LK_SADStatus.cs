using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("LK_SADStatus")]
    public class LK_SADStatus : AuditedEntity
    {
        [Required]
        [StringLength(1)]
        public string statusCode { get; set; }

        [Required]
        [StringLength(50)]
        public string statusDesc { get; set; }

         public virtual ICollection<TR_BookingHeader> TR_BookingHeader { get; set; }
    }
}
