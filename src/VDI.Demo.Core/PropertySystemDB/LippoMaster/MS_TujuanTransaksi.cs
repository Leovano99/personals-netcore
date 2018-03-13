using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    public class MS_TujuanTransaksi : AuditedEntity
    {
        public int entityID { get; set; }

        [Required]
        [StringLength(3)]
        public string tujuanTransaksiCode { get; set; }

        [Required]
        [StringLength(50)]
        public string tujuanTransaksiName { get; set; }

        public int sort { get; set; }

        public virtual ICollection<TR_BookingHeader> TR_BookingHeader { get; set; }

        public virtual ICollection<TR_UnitOrderHeader> TR_UnitOrderHeader { get; set; }

    }
}
