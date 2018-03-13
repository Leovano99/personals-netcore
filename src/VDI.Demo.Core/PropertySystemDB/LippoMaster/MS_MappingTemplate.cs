using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("MS_MappingTemplate")]
    public class MS_MappingTemplate : AuditedEntity
    {
        public int entityID { get; set; }
        public int projectID { get; set; }
        public int docID { get; set; }

        [ForeignKey("MS_DocTemplate")]
        public int docTemplateID { get; set; }
        public virtual MS_DocTemplate MS_DocTemplate { get; set; }

        public DateTime activeFrom { get; set; }
        public DateTime? activeTo { get; set; }
        public bool isTandaTerima { get; set; }
        public bool isActive { get; set; }
    }
}
