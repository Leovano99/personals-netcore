using System;
namespace VDI.Demo.MasterPlan.Unit.MS_Products.Dto
{
    public class CreateMsProductDto
    {
        public string productCode { get; set; }
        public string productName { get; set; }
        public int categoryID { get; set; }
        public bool isActive { get; set; }
    }
}
