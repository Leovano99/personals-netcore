using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_MKTAddDiscHistory")]
    public class TR_MKTAddDiscHistory : AuditedEntity
    {
        public int entityID { get; set; }

        public int bookingDetailID { get; set; }

        public short addDiscNo { get; set; }

        public double pctAddDisc { get; set; }

        [Column(TypeName = "money")]
        public decimal amtAddDisc { get; set; }

        public bool isAmount { get; set; }

        [Required]
        [StringLength(500)]
        public string addDiscDesc { get; set; }

        public byte historyNo { get; set; }
    }
}
