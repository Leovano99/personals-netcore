using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem
{
    [Table("TR_UnitReserved")]
    public class TR_UnitReserved : AuditedEntity
    {
        [ForeignKey("MS_Unit")]
        public int unitID { get; set; }
        public virtual MS_Unit MS_Unit { get; set; }

        [Required]
        [StringLength(100)]
        public string reservedBy { get; set; }

        //public string renovCode { get; set; }

        [ForeignKey("MS_Renovation")]
        public int renovID { get; set; }
        public virtual MS_Renovation MS_Renovation { get; set; }

        [ForeignKey("MS_Term")]
        public int termID { get; set; }
        public virtual MS_Term MS_Term { get; set; }

        [Column(TypeName = "money")]
        public decimal SellingPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal BFAmount { get; set; }

        public DateTime reserveDate { get; set; }

        public DateTime? releaseDate { get; set; }

        [StringLength(8)]
        public string pscode { get; set; }

        [StringLength(300)]
        public string remarks { get; set; }

        public double? disc1 { get; set; }

        public double? disc2 { get; set; }

        [StringLength(100)]
        public string specialDiscType { get; set; }

        [StringLength(100)]
        public string groupBU { get; set; }

    }
}
