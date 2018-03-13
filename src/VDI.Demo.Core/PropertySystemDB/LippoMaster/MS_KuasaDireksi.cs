using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("MS_KuasaDireksi")]
    public class MS_KuasaDireksi : AuditedEntity
    {
        public int entityID { get; set; }
        public int projectID { get; set; }

        [ForeignKey("MS_DocumentPS")]
        public int docID { get; set; }
        public virtual MS_DocumentPS MS_DocumentPS { get; set; }

        [Required]
        public string suratKuasaImage { get; set; }
        [Required]
        [StringLength(200)]
        public string remarks { get; set; }
        public bool isActive { get; set; }

        public ICollection<MS_KuasaDireksiPeople> MS_KuasaDireksiPeople { get; set; }
    }
}
