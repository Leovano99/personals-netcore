using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    [Table("MS_Cluster")]
    public class MS_Cluster : AuditedEntity
    {
        public int entityID { get; set; }

        //unique
        [Required]
        [StringLength(5)]
        public string clusterCode { get; set; }

        [Required]
        [StringLength(35)]
        public string clusterName { get; set; }

        public DateTime? dueDateDevelopment { get; set; }

        [Required]
        [StringLength(500)]
        public string dueDateRemarks { get; set; }

        public int sortNo { get; set; }

        [StringLength(100)]
        public string handOverPeriod { get; set; }

        [StringLength(100)]
        public string gracePeriod { get; set; }

        [ForeignKey("MS_Project")]
        public int projectID { get; set; }
        public virtual MS_Project MS_Project { get; set; }
        
        public double penaltyRate { get; set; }
        
        public int startPenaltyDay { get; set; }

        public ICollection<MS_Unit> MS_Unit { get; set; }

        public ICollection<MS_BankOLBooking> MS_BankOLBooking { get; set; }
    }
}
