using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_BookingHeaderTerm")]
    public class TR_BookingHeaderTerm : AuditedEntity
    {
        public int entityID { get; set; }

        [ForeignKey("TR_BookingHeader")]
        public int bookingHeaderID { get; set; }
        public virtual TR_BookingHeader TR_BookingHeader { get; set; }

        [ForeignKey("MS_Term")]
        public int termID { get; set; }
        public virtual MS_Term MS_Term { get; set; }

        public int PPJBDue { get; set; }

        [StringLength(200)]
        public string remarks { get; set; }

        public bool isActive { get; set; }

        [StringLength(1)]
        public string discBFCalcType { get; set; }

        [StringLength(1)]
        public string DPCalcType { get; set; }

        [StringLength(5)]
        public string finTypeCode { get; set; }

        public int finStartDue { get; set; }

        public int addDiscNo { get; set; }

        public double addDisc { get; set; }
    }
}
