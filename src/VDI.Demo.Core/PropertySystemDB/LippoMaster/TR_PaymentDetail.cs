using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_PaymentDetail")]
    public partial class TR_PaymentDetail : AuditedEntity
    {
        public int entityID { get; set; }

        [ForeignKey("TR_PaymentHeader")]
        public int paymentHeaderID { get; set; }
        public virtual TR_PaymentHeader TR_PaymentHeader { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int payNo { get; set; }

        //[Required]
        //[StringLength(3)]
        //public string payTypeCode { get; set; }
        [ForeignKey("LK_PayType")]
        public int payTypeID { get; set; }
        public virtual LK_PayType LK_PayType { get; set; }

        [Required]
        [StringLength(3)]
        public string othersTypeCode { get; set; }

        [Required]
        [StringLength(30)]
        public string bankName { get; set; }

        [Required]
        [StringLength(30)]
        public string chequeNo { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }

        [Required]
        [StringLength(300)]
        public string ket { get; set; }

        public DateTime dueDate { get; set; }

        //public virtual LK_PayType LK_PayType { get; set; }
        //public virtual LK_Status LK_Status { get; set; }
        
        public virtual ICollection<TR_PaymentDetailAlloc> TR_PaymentDetailAlloc { get; set; }
    }
}
