using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class UpdateNetPriceResultDto
    {
        public bool result { get; set; }

        public decimal netNetPrice { get; set; }

        public string message { get; set; }
    }
}
