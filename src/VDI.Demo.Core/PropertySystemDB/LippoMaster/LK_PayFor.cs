using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("LK_PayFor")]
    public class LK_PayFor : AuditedEntity
    {
        [Required]
        [StringLength(3)]
        public string payForCode { get; set; }

        [Required]
        [StringLength(40)]
        public string payForName { get; set; }

        public bool isSched { get; set; }

        public bool isIncome { get; set; }

        public bool isInventory { get; set; }

        public bool isSDH { get; set; }

        public bool isActive { get; set; }

        public virtual ICollection<TR_PaymentHeader> TR_PaymentHeader { get; set; }

        public virtual ICollection<LK_Alloc> LK_Alloc { get; set; }

        public virtual ICollection<SYS_RolesPayFor> SYS_RolesPayFor { get; set; }

        public virtual ICollection<TR_PaymentBulk> TR_PaymentBulk { get; set; }

        public virtual ICollection<LK_MappingTax> LK_MappingTax { get; set; }
    }
}
