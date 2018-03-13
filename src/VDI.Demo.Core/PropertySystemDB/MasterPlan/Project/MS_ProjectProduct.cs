using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("MS_ProjectProduct")]
    public class MS_ProjectProduct : AuditedEntity
    {
        [ForeignKey("MS_Project")]
        public int projectID { get; set; }
        public virtual MS_Project MS_Project { get; set; }

        [ForeignKey("MS_Product")]
        public int productID { get; set; }
        public virtual MS_Product MS_Product { get; set; }
    }
}
