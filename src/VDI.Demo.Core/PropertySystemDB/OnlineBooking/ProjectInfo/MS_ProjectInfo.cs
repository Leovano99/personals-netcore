using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.ProjectInfo
{
    [Table("MS_ProjectInfo")]
    public class MS_ProjectInfo : AuditedEntity
    {
        [ForeignKey("MS_Project")]
        public int projectID { get; set; }
        public virtual MS_Project MS_Project { get; set; }
        
        public string projectDesc { get; set; }

        [ForeignKey("MS_ProjectKeyFeaturesCollection")]
        public int keyFeaturesCollectionID { get; set; }
        public virtual MS_ProjectKeyFeaturesCollection MS_ProjectKeyFeaturesCollection { get; set; }

        [StringLength(100)]
        public string projectDeveloper { get; set; }

        [StringLength(100)]
        public string projectWebsite { get; set; }

        [StringLength(500)]
        public string projectMarketingOffice { get; set; }

        [StringLength(50)]
        public string projectMarketingPhone { get; set; }

        [StringLength(200)]
        public string projectImageLogo { get; set; }

        [StringLength(100)]
        public string sitePlansImageUrl { get; set; }

        [StringLength(1000)]
        public string sitePlansLegend { get; set; }

        public bool projectStatus { get; set; }

        public ICollection<MS_ProjectLocation> MS_ProjectLocation { get; set; }

        public ICollection<TR_ProjectImageGallery> TR_ProjectImageGallery { get; set; }
    }
}
