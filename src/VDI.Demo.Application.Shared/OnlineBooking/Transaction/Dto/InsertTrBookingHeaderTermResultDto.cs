using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class InsertTrBookingHeaderTermResultDto
    {
        public int bookingHeaderID { get; set; }

        public string remarks { get; set; }

        public int termID { get; set; }
    }
}
