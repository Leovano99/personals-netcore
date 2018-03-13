using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_PaymentHeader")]
    public class TR_PaymentHeader : AuditedEntity
    {
        public int entityID { get; set; }

        [ForeignKey("MS_Account")]
        public int accountID { get; set; }
        public virtual MS_Account MS_Account { get; set; }

        [Required]
        [StringLength(18)]
        public string transNo { get; set; }

        [Required]
        [StringLength(18)]
        public string controlNo { get; set; }

        [ForeignKey("TR_BookingHeader")]
        public int? bookingHeaderID { get; set; }
        public virtual TR_BookingHeader TR_BookingHeader { get; set; }

        [Required]
        [StringLength(1)]
        public string combineCode { get; set; }

        public DateTime paymentDate { get; set; }

        public DateTime? clearDate { get; set; }

        [Required]
        [StringLength(300)]
        public string ket { get; set; }

        [ForeignKey("LK_PayFor")]
        public  int? payForID{ get; set; }
        public virtual LK_PayFor LK_PayFor { get; set; }
        
        public DateTime? apvTime { get; set; }

        [StringLength(50)]
        public string apvUN { get; set; }

        public DateTime? SMSSentTime { get; set; }

        public bool isSMS { get; set; }

        public bool? hadmail { get; set; }

        public DateTime? mailTime { get; set; }

        [StringLength(1)]
        public string isFP { get; set; } //1 = done, 2 = kehabisan FP, 3 = tidak TAX

        public virtual ICollection<TR_PaymentDetail> TR_PaymentDetail { get; set; }
    }
}
