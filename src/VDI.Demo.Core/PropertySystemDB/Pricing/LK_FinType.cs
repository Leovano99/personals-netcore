using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDI.Demo.PropertySystemDB.Pricing
{
    [Table("LK_FinType")]
    public class LK_FinType : AuditedEntity
    {
        //unique
        [Required]
        [StringLength(5)]
        public string finTypeCode { get; set; }

        [Required]
        [StringLength(50)]
        public string finTypeDesc { get; set; }

        [Required]
        public short finTimes { get; set; }

        [Required]
        public double pctComm { get; set; }

        [Required]
        public bool isCommStd { get; set; }

        [Required]
        public bool isCashStd { get; set; }

        [Required]
        [StringLength(3)]
        public string oldFinTypeCode { get; set; }

        [Required]
        public double pctCommLC { get; set; }

        [Required]
        public double pctCommTB { get; set; }

        public ICollection<MS_TermPmt> MS_TermPmt { get; set; }
    }
}
