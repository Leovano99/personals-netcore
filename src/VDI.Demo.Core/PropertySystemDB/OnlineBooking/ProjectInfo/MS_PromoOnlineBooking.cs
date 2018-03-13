using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.ProjectInfo
{
    [Table("MS_PromoOnlineBooking")]
    public class MS_PromoOnlineBooking : AuditedEntity
    {
        [ForeignKey("MS_Project")]
        public int projectID { get; set; }
        public virtual MS_Project MS_Project { get; set; }

        [Required]
        [StringLength(200)]
        public string promoFile { get; set; }

        [Required]
        [StringLength(500)]
        public string promoAlt { get; set; }

        [Required]
        [StringLength(10)]
        public string promoDataType { get; set; }

        [Required]
        [StringLength(200)]
        public string targetURL { get; set; }

        public bool isActive { get; set; }
    }
}
