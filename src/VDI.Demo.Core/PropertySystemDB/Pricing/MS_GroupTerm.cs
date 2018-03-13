using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDI.Demo.PropertySystemDB.Pricing
{
    [Table("MS_GroupTerm")]
    public class MS_GroupTerm : AuditedEntity
    {
        //unique
        [Required]
        [StringLength(5)]
        public string GroupTermCode { get; set; }

        [Required]
        [StringLength(20)]
        public string GroupTermDesc { get; set; }

        public int entityID { get; set; }
    }
}
