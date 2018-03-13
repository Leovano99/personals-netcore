using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_BookingDocument")]
    public class TR_BookingDocument : AuditedEntity
    {
        public int entityID { get; set; }
        
        [ForeignKey("TR_BookingHeader")]
        public int bookingHeaderID { get; set; }
        public virtual TR_BookingHeader TR_BookingHeader { get; set; }

        //[Required]
        //[StringLength(20)]
        //public string bookCode { get; set; }

        [ForeignKey("MS_DocumentPS")]
        public int docID { get; set; }
        public virtual MS_DocumentPS MS_DocumentPS { get; set; }

        //[Required]
        //[StringLength(5)]
        //public string docCode { get; set; }

        [Required]
        [StringLength(50)]
        public string docNo { get; set; }

        public DateTime docDate { get; set; }

        [Required]
        [StringLength(255)]
        public string remarks { get; set; }
        
        [StringLength(50)]
        public string oldDocNo { get; set; }

        [StringLength(50)]
        public string tandaTerimaNo { get; set; }

        public DateTime? tandaTerimaDate { get; set; }

        [StringLength(100)]
        public string tandaTerimaBy { get; set; }

        [StringLength(150)]
        public string tandaTerimaFile { get; set; }

    }
}
