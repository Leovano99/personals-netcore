using System;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_BookingDetailAdjustment")]
    public class TR_BookingDetailAdjustment: AuditedEntity
    {
        public int entityID { get; set; }

        [ForeignKey("TR_BookingDetail")]
        public int bookingDetailID { get; set; }
        public virtual TR_BookingDetail TR_BookingDetail { get; set; }

        //[Required]
        //[StringLength(20)]
        //public string bookCode { get; set; }

        //[Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int refNo { get; set; }
        
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int adjNo { get; set; }
        
        public DateTime adjDate { get; set; }
        
        public DateTime area { get; set; }

        [Required]
        [StringLength(255)]
        public string remarks { get; set; }
    }
}
