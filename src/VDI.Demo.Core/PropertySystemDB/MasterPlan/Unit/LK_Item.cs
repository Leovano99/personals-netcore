using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.LippoMaster;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    [Table("LK_Item")]
    public class LK_Item : AuditedEntity
    {
        //unique
        [Required]
        [StringLength(2)]
        public string itemCode { get; set; }

        [Required]
        [StringLength(40)]
        public string itemName { get; set; }

        [Required]
        [StringLength(15)]
        public string shortName { get; set; }

        [Required]
        public int sortNo { get; set; }

        [Required]
        public bool isOption { get; set; }

        [Required]
        public int optionSort { get; set; }

        public ICollection<MS_UnitItem> MS_UnitItem { get; set; }
        public ICollection<TR_BookingSalesDisc> TR_BookingSalesDisc { get; set; }
        public ICollection<TR_BookingDetail> TR_BookingDetail { get; set; }
        public ICollection<TR_BookingItemPrice> TR_BookingItemPrice { get; set; }
    }
}
