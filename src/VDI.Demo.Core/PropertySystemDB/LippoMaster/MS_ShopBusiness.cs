using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("MS_ShopBusiness")]
    public partial class MS_ShopBusiness : AuditedEntity
    {
        public int entityID { get; set; }

        [Required]
        [StringLength(3)]
        public string shopBusinessCode { get; set; }

        [Required]
        [StringLength(50)]
        public string shopBusinessName { get; set; }

        public int sort { get; set; }

        public virtual ICollection<TR_BookingHeader> TR_BookingHeader { get; set; }
    }
}
