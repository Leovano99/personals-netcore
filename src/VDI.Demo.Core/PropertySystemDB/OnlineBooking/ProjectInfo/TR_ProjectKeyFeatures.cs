using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.ProjectInfo
{
    [Table("TR_ProjectKeyFeatures")]
    public class TR_ProjectKeyFeatures : AuditedEntity
    {   
        [Required]
        [StringLength(1000)]
        public string keyFeatures { get; set; }

        public int status { get; set; }

        [ForeignKey("MS_ProjectKeyFeaturesCollection")]
        public int keyFeaturesCollectionID { get; set; }
        public virtual MS_ProjectKeyFeaturesCollection MS_ProjectKeyFeaturesCollection { get; set; }

    }
}