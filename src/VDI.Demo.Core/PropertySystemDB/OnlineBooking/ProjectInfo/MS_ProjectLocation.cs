using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.ProjectInfo
{
    [Table("MS_ProjectLocation")]
    public class MS_ProjectLocation : AuditedEntity
    {
        public double latitude { get; set; }

        public double longitude { get; set; }

        [StringLength(500)]
        public string projectAddress { get; set; }

        [StringLength(100)]
        public string locationImageURL { get; set; }

        [ForeignKey("MS_ProjectInfo")]
        public int projectInfoID { get; set; }
        public virtual MS_ProjectInfo MS_ProjectInfo { get; set; }
    }
}