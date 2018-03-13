using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_PaymentDetailAlloc")]
    public partial class TR_PaymentDetailAlloc : AuditedEntity
    {
        public int entityID { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short schedNo { get; set; }

        [Column(TypeName = "money")]
        public decimal netAmt { get; set; }

        [Column(TypeName = "money")]
        public decimal vatAmt { get; set; }

        [ForeignKey("TR_PaymentDetail")]
        public int paymentDetailID { get; set; }
        public virtual TR_PaymentDetail TR_PaymentDetail { get; set; }
    }
}
