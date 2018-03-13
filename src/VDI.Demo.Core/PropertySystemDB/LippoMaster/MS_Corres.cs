using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("MS_Corres")]
    public class MS_Corres : AuditedEntity
    {
        public int entityID { get; set; }

        [Required]
        [StringLength(5)]
        public string corresCode { get; set; }

        [Required]
        [StringLength(50)]
        public string corresName { get; set; }

        public ICollection<TR_BookingCorres> TR_BookingCorres { get; set; }
    }
}
