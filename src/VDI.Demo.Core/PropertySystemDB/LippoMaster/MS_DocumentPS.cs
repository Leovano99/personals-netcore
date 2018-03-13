using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("MS_Document")]
    public class MS_DocumentPS : AuditedEntity
    {
        public int entityID { get; set; }

        [Required]
        [StringLength(5)]
        public string docCode { get; set; }

        [Required]
        [StringLength(50)]
        public string docName { get; set; }
        
        public ICollection<MS_KuasaDireksi> MS_KuasaDireksi { get; set; }
        public ICollection<MS_DocTemplate> MS_DocTemplate { get; set; }
        public ICollection<TR_BookingDocument> TR_BookingDocument { get; set; }
        public ICollection<DocNo_Counter> DocNo_Counter { get; set; }
    }
}
