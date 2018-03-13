using System;
namespace VDI.Demo.MasterPlan.Unit.MS_Products.Dto
{
    public class GetAllProductListDto
    {
        public int Id { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public bool isActive { get; set; }
    }
}
