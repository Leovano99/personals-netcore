using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class ListNetPriceCommResultDto
    {
        public decimal netPriceComm { get; set; }

        public decimal netPriceCash { get; set; }

        public decimal amountComm { get; set; }

        public int bookingDetailID { get; set; }
    }

    public class ListNetPrice
    {
        public decimal netPrice { get; set; }

        public int bookingDetailID { get; set; }

        public decimal amount { get; set; }

    }
}
