using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.PaymentMidtrans.Dto
{
    public class RequestTokenResultDto
    {
        public string token { get; set; }
        public string redirec_url { get; set; }
    }
}
