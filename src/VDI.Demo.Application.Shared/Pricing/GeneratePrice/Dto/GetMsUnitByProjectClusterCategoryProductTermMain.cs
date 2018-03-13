using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.GeneratePrice.Dto
{
    public class GetMsUnitByProjectClusterCategoryProductTermMain
    {
        public int projectID { get; set; }
        public int clusterID { get; set; }
        public int categoryID { get; set; }
        public int productID { get; set; }
        public int termMainID { get; set; }
    }
}
