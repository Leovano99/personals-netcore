using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Unit
{
    [Table("MS_UnitItem")]
    public class MS_UnitItem : AuditedEntity
    {
        public int entityID { get; set; }

        [ForeignKey("MS_Unit")]
        public int unitID { get; set; }
        public virtual MS_Unit MS_Unit { get; set; }

        [ForeignKey("LK_Item")]
        public int itemID { get; set; }
        public virtual LK_Item LK_Item { get; set; }

        [Required]
        [StringLength(5)]
        public string coCode { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal amount { get; set; }

        [Required]
        public double pctDisc { get; set; }

        [Required]
        public double pctTax { get; set; }

        [Required]
        public double area { get; set; }

        [Required]
        [StringLength(50)]
        public string dimension { get; set; }

        public ICollection<MS_UnitRoom> MS_UnitRoom { get; set; }
    }
}
