using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDI.Demo.PropertySystemDB.Pricing
{
    [Table("MS_Discount")]
    public class MS_Discount : AuditedEntity
    {
        [Required]
        public string discountCode { get; set; }

        [Required]
        public string discountName { get; set; }

        public bool isActive { get; set; }

        public ICollection<MS_TermAddDisc> MS_TermAddDisc { get; set; }
    }
}
