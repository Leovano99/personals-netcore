using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDI.Demo.PropertySystemDB.Pricing
{
    [Table("TR_BasePrice")]
    public class TR_BasePrice : AuditedEntity
    {
        [StringLength(5)]
        public string projectCode { get; set; }

        [StringLength(20)]
        public string roadCode { get; set; }

        [StringLength(20)]
        public string unitCode { get; set; }

        [StringLength(8)]
        public string unitNo { get; set; }

        public double unitBasePrice { get; set; }
    }
}
