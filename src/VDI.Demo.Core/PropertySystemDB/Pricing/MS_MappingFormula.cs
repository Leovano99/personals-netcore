using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;

namespace VDI.Demo.PropertySystemDB.Pricing
{
    [Table("MS_MappingFormula")]
    public class MS_MappingFormula : AuditedEntity
    {
        [ForeignKey("MS_Project")]
        public int projectID { get; set; }
        public virtual MS_Project MS_Project { get; set; }

        [ForeignKey("MS_Category")]
        public int categoryID { get; set; }
        public virtual MS_Category MS_Category { get; set; }

        [ForeignKey("MS_Product")]
        public int productID { get; set; }
        public virtual MS_Product MS_Product { get; set; }

        [ForeignKey("MS_FormulaCode")]
        public int formulaCodeID { get; set; }
        public virtual MS_FormulaCode MS_FormulaCode { get; set; }
    }
}
