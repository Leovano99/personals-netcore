using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_PenaltySchedule")]
    public class TR_PenaltySchedule : CreationAuditedEntity
    {
        public int entityID { get; set; }
        
        [ForeignKey("TR_BookingHeader")]
        public int bookingHeaderID { get; set; }
        public virtual TR_BookingHeader TR_BookingHeader { get; set; }

        public int SeqNo { get; set; }
        
        public int ScheduleTerm { get; set; }

        [StringLength(50)]
        public string ScheduleAllocCode { get; set; }

        [Column(TypeName = "money")]
        public decimal? ScheduleNetAmount { get; set; }

        public int? penaltyAging { get; set; }

        [Column(TypeName = "money")]
        public decimal? penaltyAmount { get; set; }

        [StringLength(100)]
        public string createdBy { get; set; }

        public DateTime? createdOn { get; set; }

    }
}
