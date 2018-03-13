using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    public class MS_TermDiscOnlineBooking : AuditedEntity
    {
        [ForeignKey("MS_Term")]
        public int termID { get; set; }
        public virtual MS_Term MS_Term { get; set; }

        [StringLength(50)]
        public string discName { get; set; }

        public double pctDisc { get; set; }
    }
}
