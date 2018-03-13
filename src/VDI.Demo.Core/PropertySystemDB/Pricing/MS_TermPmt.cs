using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDI.Demo.PropertySystemDB.Pricing
{
    [Table("MS_TermPmt")]
    public class MS_TermPmt : AuditedEntity
    {
        public short termNo { get; set; }

        public short finStartDue { get; set; }

        public short? finStartM { get; set; }

        public int entityID { get; set; }

        [ForeignKey("MS_Term")]
        public int termID { get; set; }
        public virtual MS_Term MS_Term { get; set; }

        [ForeignKey("LK_FinType")]
        public int finTypeID { get; set; }
        public virtual LK_FinType LK_FinType { get; set; }

        public bool isSetting { get; set; }
    }
}
