using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("DocNo_Counter")]
    public class DocNo_Counter : AuditedEntity
    {
        [ForeignKey("MS_Project")]
        public int projectID { get; set; }
        public virtual MS_Project MS_Project { get; set; }

        [ForeignKey("MS_DocumentPS")]
        public int docID { get; set; }
        public virtual MS_DocumentPS MS_DocumentPS { get; set; }

        [ForeignKey("MS_Company")]
        public int coID { get; set; }
        public virtual MS_Company MS_Company { get; set; }

        public int docNo { get; set; }
    }
}
