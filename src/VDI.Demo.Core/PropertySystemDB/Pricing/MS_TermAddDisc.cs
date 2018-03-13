using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDI.Demo.PropertySystemDB.Pricing
{
    [Table("MS_TermAddDisc")]
    public class MS_TermAddDisc : AuditedEntity
    {
        [Column(Order = 2)]
        public short termNo { get; set; }

        public byte addDiscNo { get; set; }

        public double addDiscPct { get; set; }

        public decimal addDiscAmt { get; set; }

        public int entityID { get; set; }

        [ForeignKey("MS_Term")]
        public int termID { get; set; }
        public virtual MS_Term MS_Term { get; set; }

        [ForeignKey("MS_Discount")]
        public int discountID { get; set; }
        public virtual MS_Discount MS_Discount { get; set; }
    }
}
