using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_BookingItemPrice")]
    public class TR_BookingItemPrice : AuditedEntity
    {
        public int entityID { get; set; }
        
        [ForeignKey("TR_BookingHeader")]
        public int bookingHeaderID { get; set; }
        public virtual TR_BookingHeader TR_BookingHeader { get; set; }

        //[Required]
        //[StringLength(20)]
        //public string bookCode { get; set; }

        [Required]
        [StringLength(2)]
        public string renovCode { get; set; }
        
        [ForeignKey("LK_Item")]
        public int itemID { get; set; }
        public virtual LK_Item LK_Item { get; set; }

        [ForeignKey("MS_Term")]
        public int? termID { get; set; }
        public virtual MS_Term MS_Term { get; set; }

        [Column(TypeName = "money")]
        public decimal grossPrice { get; set; }

    }
}
