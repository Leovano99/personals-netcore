using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Project.Dto
{
    public class ListUnitTypeByProjectIdResultDto
    {
        public int unitTypeID { get; set; }
        public string unitType { get; set; }
        public string message { get; set; }
        public int clusterID { get; set; }
        public string clusterName { get; set; }
        public string image { get; set; }
        public double area { get; set; }
    }
}
