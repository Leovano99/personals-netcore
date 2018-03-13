using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.ProjectInfo
{
    [Table("MS_ProjectKeyFeaturesCollection")]
    public class MS_ProjectKeyFeaturesCollection : AuditedEntity
    {
        public ICollection<TR_ProjectKeyFeatures> TR_ProjectKeyFeatures { get; set; }
        public ICollection<MS_ProjectInfo> MS_ProjectInfo { get; set; }

    }
}