using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_Terms.Dto
{
    public class GetTermNoDropdownByProjectClusterCategoryProductInputDto
    {
        public int projectID { get; set; }
        public int clusterID { get; set; }
        public int categoryID { get; set; }
        public int productID { get; set; }
    }
}
