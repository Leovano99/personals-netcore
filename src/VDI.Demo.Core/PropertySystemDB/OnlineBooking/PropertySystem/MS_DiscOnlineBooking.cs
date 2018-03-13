using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem
{
    [Table("MS_DiscOnlineBooking")]
    public class MS_DiscOnlineBooking : AuditedEntity
    {
        [ForeignKey("MS_Project")]
        public int projectID { get; set; }
        public virtual MS_Project MS_Project { get; set; }

        [ForeignKey("MS_Cluster")]
        public int? clusterID { get; set; }
        public virtual MS_Cluster MS_Cluster { get; set; }

        [Required]
        [StringLength(3)]
        public string batchPP { get; set; }

        public float discPct { get; set; }
        
        [StringLength(200)]
        public string discDesc { get; set; }

        [StringLength(300)]
        public string nameLabel { get; set; }
    }
}
