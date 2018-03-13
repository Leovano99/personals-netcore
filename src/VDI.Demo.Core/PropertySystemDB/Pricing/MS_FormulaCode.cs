using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDI.Demo.PropertySystemDB.Pricing
{
    [Table("MS_FormulaCode")]
    public class MS_FormulaCode : AuditedEntity
    {
        //unique
        [Required]
        public int formulaCode { get; set; }

        public int formulaNo { get; set; }

        [StringLength(20)]
        public string formulaName { get; set; }

        [StringLength(100)]
        public string formula { get; set; }

        public ICollection<MS_MappingFormula> MS_MappingFormula { get; set; }
    }
}
