using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.ProjectInfo
{
    [Table("TR_ProjectImageGallery")]
    public class TR_ProjectImageGallery : AuditedEntity
    {
        [StringLength(100)]
        public string imageURL { get; set; }

        [StringLength(500)]
        public string imageAlt { get; set; }

        public bool imageStatus { get; set; }

        [ForeignKey("MS_ProjectInfo")]
        public int projectInfoID { get; set; }
        public virtual MS_ProjectInfo MS_ProjectInfo { get; set; }
    }
}
