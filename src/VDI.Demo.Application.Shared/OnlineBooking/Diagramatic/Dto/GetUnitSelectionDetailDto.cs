using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Diagramatic.Dto
{
    public class GetUnitSelectionDetailDto
    {
        public string projectName { get; set; }
        public string tower { get; set; }
        public string unitCode { get; set; }
        public string unitNo { get; set; }
        public decimal bfAmount { get; set; }
    }
}
