using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class InsertTrBookingTaxInputDto
    {
        public int bookingDetailID { get; set; }

        public int unitID { get; set; }

        public int termID { get; set; }

        public decimal sellingPrice { get; set; }

        public decimal netNetPrice { get; set; }
    }
}
