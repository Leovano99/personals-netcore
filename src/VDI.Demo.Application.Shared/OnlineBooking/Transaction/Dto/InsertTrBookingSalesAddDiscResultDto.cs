using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class InsertTrBookingSalesAddDiscResultDto
    {
        public int bookingHeaderID { get; set; }

        public double pctTax { get; set; }

        public int itemID { get; set; }
    }
}
