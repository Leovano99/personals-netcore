using System;
namespace VDI.Demo.MasterPlan.Unit.MS_Categories.Dto
{
    public class GetAllCategoryListDto
    {
        public int categoryID { get; set; }
        public string categoryName { get; set; }
        public string categoryCode { get; set; }
        public Boolean isActive { get; set; }
    }
}
