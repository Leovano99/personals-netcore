using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.TAXDB
{
    [Table("FP_LK_FPTransCode")]
    public class FP_LK_FPTransCode : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return FPTransCode;
            }
            set { /* nothing */ }
        }

        [Key]
        [StringLength(2)]
        public string FPTransCode { get; set; }

        [Required]
        [StringLength(20)]
        public string FPTransDesc { get; set; }
    }
}
