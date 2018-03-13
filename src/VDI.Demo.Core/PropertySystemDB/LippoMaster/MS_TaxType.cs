using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("MS_TaxType")]
    public class MS_TaxType : AuditedEntity
    {
        [StringLength(5)]
        public string taxTypeCode { get; set; }

        [Required]
        [StringLength(200)]
        public string taxTypeDesc { get; set; }

        public ICollection<TR_BookingTax> TR_BookingTax { get; set; }
    }
}
