using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_BookingCorres")]
    public class TR_BookingCorres: AuditedEntity
    {
        public int entityID { get; set; }

        [ForeignKey("TR_BookingHeader")]
        public int bookingHeaderID { get; set; }
        public virtual TR_BookingHeader TR_BookingHeader { get; set; }

        //[Required]
        //[StringLength(20)]
        //public string bookCode { get; set; }
        
        public short corresNo { get; set; }

        [ForeignKey("MS_Corres")]
        public int corresId { get; set; }
        public virtual MS_Corres MS_Corres { get; set; }

        //[Required]
        //[StringLength(5)]
        //public string corresCode { get; set; }
        
        public DateTime corresDate { get; set; }

        [Required]
        [StringLength(40)]
        public string refNo { get; set; }
        
        public DateTime mailDate { get; set; }
        
        public DateTime dueDate { get; set; }

        [Required]
        [StringLength(100)]
        public string recepient { get; set; }

        [Required]
        [StringLength(300)]
        public string remarks { get; set; }
    }
}
