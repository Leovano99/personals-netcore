using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Unit.MS_Products.Dto
{
    public class GetMsProductDropdownByProjectClusterCategoryInputDto
    {
        public int projectID { get; set; }
        public int clusterID { get; set; }
        public int categoryID { get; set; }
    }
}
