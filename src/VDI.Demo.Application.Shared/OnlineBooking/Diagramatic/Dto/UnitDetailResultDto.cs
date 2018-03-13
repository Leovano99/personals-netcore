using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Diagramatic.Dto
{
    public class UnitDetailResultDto
    {
        public int unitID { get; set; }
        public string unitCode { get; set; }
        public string unitNo { get; set; }
        public decimal bookingFee { get; set; }

        public List<ListRenovResultDto> ListRenov { get; set; }
    }
    public class ListRenovResultDto
    {
        public int renovID { get; set; }
        public string renovCode { get; set; }
        public string renovName { get; set; }
    }
}
