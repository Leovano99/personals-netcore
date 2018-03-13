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
    public class MS_BankOLBooking : AuditedEntity
    {
        [ForeignKey("MS_Project")]
        public int projectID { get; set; }
        public virtual MS_Project MS_Project { get; set; }

        [ForeignKey("MS_Cluster")]
        public int clusterID { get; set; }
        public virtual MS_Cluster MS_Cluster { get; set; }

        [Required]
        [StringLength(100)]
        public string bankName { get; set; }

        [Required]
        [StringLength(100)]
        public string bankRekNo { get; set; }

        [Required]
        [StringLength(150)]
        public string bankRekName { get; set; }
    }
}
