using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("MS_KuasaDireksiPeople")]
    public class MS_KuasaDireksiPeople : AuditedEntity
    {
        public int entityID { get; set; }

        [ForeignKey("MS_KuasaDireksi")]
        public int kuasaDireksiID { get; set; }
        public virtual MS_KuasaDireksi MS_KuasaDireksi { get; set; }

        [StringLength(250)]
        public string signeeName { get; set; }
        [StringLength(250)]
        public string signeePosition { get; set; }
        [StringLength(250)]
        public string signeeEmail { get; set; }
        [StringLength(20)]
        public string signeePhone { get; set; }
        public string signeeSignImage { get; set; }
        public bool isActive { get; set; }
        public int? officerID { get; set; }
    }
}
