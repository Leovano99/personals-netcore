using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.NewCommDB
{
    [Table("MS_GroupCommPct")]
    public class MS_GroupCommPct : AuditedEntity
    {

        public Byte asUplineNo { get; set; }

        public DateTime validDate { get; set; }

        [Column(TypeName = "money")]
        public decimal minAmt { get; set; }

        [Column(TypeName = "money")]
        public decimal maxAmt { get; set; }

        public double? commPctPaid { get; set; }

        public double commPctHold { get; set; }

        [Column(TypeName = "money")]
        public decimal? nominal { get; set; } //di table newcomm tidak ada

        [ForeignKey("MS_StatusMember")]
        public int statusMemberID { get; set; }
        public virtual MS_StatusMember MS_StatusMember { get; set; }

        [ForeignKey("LK_CommType")]
        public int commTypeID { get; set; }
        public virtual LK_CommType LK_CommType { get; set; }

        [ForeignKey("MS_GroupSchema")]
        public int groupSchemaID { get; set; }
        public virtual MS_GroupSchema MS_GroupSchema { get; set; }

        public int entityID { get; set; }

        public bool isComplete { get; set; }
    }
}
