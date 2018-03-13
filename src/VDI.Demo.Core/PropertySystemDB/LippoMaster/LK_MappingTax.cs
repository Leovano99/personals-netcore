using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("LK_MappingTax")]
    public class LK_MappingTax : AuditedEntity
    {
        [ForeignKey("LK_PayFor")]
        public int payForID { get; set; }
        public virtual LK_PayFor LK_PayFor { get; set; }

        [ForeignKey("LK_PayType")]
        public int payTypeID { get; set; }
        public virtual LK_PayType LK_PayType { get; set; }

        [ForeignKey("LK_OthersType")]
        public int othersTypeID { get; set; }
        public virtual LK_OthersType LK_OthersType { get; set; }

        public bool isVAT { get; set; }

        public bool isActive { get; set; }
    }
}
