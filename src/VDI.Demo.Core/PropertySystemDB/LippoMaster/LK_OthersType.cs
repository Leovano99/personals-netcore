using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    public class LK_OthersType : AuditedEntity
    {
        [Required]
        [StringLength(3)]
        public string othersTypeCode { get; set; }

        [Required]
        [StringLength(50)]
        public string othersTypeDesc { get; set; }
        public bool isOthers { get; set; }
        public bool isOTP { get; set; }
        public bool isPayment { get; set; }
        public bool isAdjSAD { get; set; }
        public short sortNum { get; set; }
        public bool isSDH { get; set; }
        public bool isActive { get; set; }

        public virtual ICollection<SYS_RolesOthersType> SYS_RolesOthersType { get; set; }

        public virtual ICollection<TR_PaymentBulk> TR_PaymentBulk { get; set; }

        public virtual ICollection<LK_MappingTax> LK_MappingTax { get; set; }
    }
}
