using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class InsertTrBookingItemPriceResultDto
    {
        public int bookingHeaderID { get; set; }

        public decimal grossPrice { get; set; }

        public int itemID { get; set; }

        public int termID { get; set; }

        public string renovCode { get; set; }
    }
}
