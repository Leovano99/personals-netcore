using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_SSPDetail")]
    public class TR_SSPDetail : AuditedEntity
    {
        public int entityID { get; set; }

        [Required]
        [StringLength(8)]
        public string psCode { get; set; }

        [Required]
        [StringLength(100)]
        public string psName { get; set; }

        [Required]
        [StringLength(50)]
        public string psCity { get; set; }

        [Required]
        [StringLength(100)]
        public string unitDesc { get; set; }
        
        public double landArea { get; set; }
        
        public double buildArea { get; set; }

        [Column(TypeName = "money")]
        public decimal netAmt { get; set; }

        [Column(TypeName = "money")]
        public decimal pphAmt { get; set; }

        [ForeignKey("TR_SSPHeader")]
        public int SSPHeaderID { get; set; }
        public virtual TR_SSPHeader TR_SSPHeader { get; set; }

        [ForeignKey("MS_Account")]
        public int accountID { get; set; }
        public virtual MS_Account MS_Account { get; set; }

        [ForeignKey("TR_BookingDetail")]
        public int bookingDetailID { get; set; }
        public virtual TR_BookingDetail TR_BookingDetail { get; set; }
    }
}
