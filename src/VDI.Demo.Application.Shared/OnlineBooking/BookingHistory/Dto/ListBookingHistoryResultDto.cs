using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.BookingHistory.Dto
{
    public class ListBookingHistoryResultDto
    {
        public int statusID { get; set; }
        public int orderID { get; set; }
        public string orderCode { get; set; }
        public DateTime orderDate { get; set; }
        public string psName { get; set; }
        public string status { get; set; }
    }
}
