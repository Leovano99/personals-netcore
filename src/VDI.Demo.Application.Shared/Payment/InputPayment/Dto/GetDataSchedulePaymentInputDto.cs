using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class GetDataSchedulePaymentInputDto
    {
        public int payForID { get; set; }
        public int bookingHeaderID { get; set; }
        public int accountID { get; set; }
    }
}
