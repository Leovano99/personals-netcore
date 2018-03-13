using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("MS_DocTemplate")]
    public class MS_DocTemplate : AuditedEntity
    {
        public int entityID { get; set; }

        [ForeignKey("MS_DocumentPS")]
        public int docID { get; set; }
        public virtual MS_DocumentPS MS_DocumentPS { get; set; }

        [Required]
        [StringLength(100)]
        public string templateName { get; set; }
        [Required]
        public string templateFile { get; set; }
        public bool? isMaster { get; set; }
        public bool? isActive { get; set; }

        public ICollection<MS_MappingTemplate> MS_MappingTemplate { get; set; }
    }
}
