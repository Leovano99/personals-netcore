using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class UpdateOrderStatusFullyPaid
    {
        public int orderHeaderID { get; set; }

        public int bookingHeaderID { get; set; }

    }
}
