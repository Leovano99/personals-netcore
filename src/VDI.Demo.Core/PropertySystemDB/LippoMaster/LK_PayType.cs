using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("LK_PayType")]
    public class LK_PayType : AuditedEntity
    {
        public int entityID { get; set; }

        [Required]
        [StringLength(3)]
        public string payTypeCode { get; set; }

        [Required]
        [StringLength(30)]
        public string payTypeDesc { get; set; }

        public bool isIncome { get; set; }
        public bool isInventory { get; set; }
        public bool isBooking { get; set; }
        public bool isActive { get; set; }

        public ICollection<TR_PaymentDetail> TR_PaymentDetail { get; set; }

        public virtual ICollection<SYS_RolesPayType> SYS_RolesPayType { get; set; }

        public virtual ICollection<TR_PaymentBulk> TR_PaymentBulk { get; set; }

        public virtual ICollection<LK_MappingTax> LK_MappingTax { get; set; }
    }
}
