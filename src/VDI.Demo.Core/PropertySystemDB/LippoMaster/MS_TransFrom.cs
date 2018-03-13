using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("MS_TransFrom")]
    public partial class MS_TransFrom : AuditedEntity
    {
        public int entityID { get; set; }

        [Required]
        [StringLength(5)]
        public string transCode { get; set; }

        [Required]
        [StringLength(5)]
        public string parentTransCode { get; set; }

        [Required]
        [StringLength(40)]
        public string transName { get; set; }

        [Required]
        [StringLength(100)]
        public string transDesc { get; set; }

        public virtual ICollection<TR_BookingHeader> TR_BookingHeader { get; set; }
    }
}
