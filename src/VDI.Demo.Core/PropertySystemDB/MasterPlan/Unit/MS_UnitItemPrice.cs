using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    [Table("MS_UnitItemPrice")]
    public class MS_UnitItemPrice : AuditedEntity
    {
        [ForeignKey("MS_Renovation")]
        public int renovID { get; set; }
        public virtual MS_Renovation MS_Renovation { get; set; }

        [ForeignKey("MS_Term")]
        public int termID { get; set; }
        public virtual MS_Term MS_Term { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal grossPrice { get; set; }

        public DateTime? execTime { get; set; }

        [StringLength(50)]
        public string execUN { get; set; }

        [StringLength(50)]
        public string execMode { get; set; }

        public int entityID { get; set; }

        [ForeignKey("MS_Unit")]
        public int unitID { get; set; }
        public virtual MS_Unit MS_Unit { get; set; }

        [ForeignKey("LK_Item")]
        public int itemID { get; set; }
        public virtual LK_Item LK_Item { get; set; }

    }
}
