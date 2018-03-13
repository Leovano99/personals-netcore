using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Project.Dto
{
    public class ListUnitTypeByClusterResultDto
    {
        public int unitTypeID { get; set; }
        public string unitType { get; set; }
        public string message { get; set; }
        public double area { get; set; }
        public string bedroom { get; set; }
        public string detailImage { get; set; }
    }
}
