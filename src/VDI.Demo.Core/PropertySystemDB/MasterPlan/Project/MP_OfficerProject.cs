using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("MP_OfficerProject")]
    public class MP_OfficerProject : AuditedEntity
    {
        [ForeignKey("MS_Project")]
        public int projectID { get; set; }
        public virtual MS_Project MS_Project { get; set; }

        [ForeignKey("MS_Officer")]
        public int officerID { get; set; }
        public virtual MS_Officer MS_Officer { get; set; }

        public string whatsappNo { get; set; }

        public string phoneNo { get; set; }

        public string email { get; set; }
    }
}
