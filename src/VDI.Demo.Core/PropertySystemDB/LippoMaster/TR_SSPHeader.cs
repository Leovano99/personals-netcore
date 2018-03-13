using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_SSPHeader")]
    public class TR_SSPHeader : AuditedEntity
    {
        public int entityID { get; set; }

        [Required]
        [StringLength(20)]
        public string batchNo { get; set; }

        public DateTime generateDate { get; set; }

        [Required]
        [StringLength(5)]
        public string coCode { get; set; }

        [Required]
        [StringLength(20)]
        public string period { get; set; }

        public virtual ICollection<TR_SSPDetail> TR_SSPDetail { get; set; }
    }
}
