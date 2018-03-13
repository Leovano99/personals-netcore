using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("MP_CompanyProject")]
    public class MP_CompanyProject : AuditedEntity
    {
        [ForeignKey("MS_Company")]
        public int companyID { get; set; }
        public virtual MS_Company MS_Company { get; set; }

        [ForeignKey("MS_Project")]
        public int projectID { get; set; }
        public virtual MS_Project MS_Project { get; set; }

        public bool mainStatus { get; set; }

    }
}
