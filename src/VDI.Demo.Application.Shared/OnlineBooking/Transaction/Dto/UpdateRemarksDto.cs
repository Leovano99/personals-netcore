using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class UpdateRemarksDto
    {
        public int bookingHeaderID { get; set; }

        public decimal sellingPrice { get; set; }
    }
}
