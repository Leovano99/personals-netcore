using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Diagramatic.Dto
{
    public class ListBookingHistoryResultDto
    {
        public string orderCode { get; set; }
        public DateTime orderDate { get; set; }
        public string psName { get; set; }
        public int status { get; set; }
    }
}
