using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("MS_MappingDocument")]
    public class MS_MappingDocument : AuditedEntity
    {
        public int entityID { get; set; }

        [Required]
        [StringLength(50)]
        public string payMethod { get; set; }

        [Required]
        [StringLength(25)]
        public string documentType { get; set; }
    }
}
