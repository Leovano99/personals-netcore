using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class TrBookingHeaderResultDto
    {
        public int bookingHeaderID { get; set; }

        public string termRemarks { get; set; }

        public int unitID { get; set; }

        public int termID { get; set; }

        public DateTime bookDate { get; set; }

        public string message { get; set; }

        public bool result { get; set; }
    }
}
