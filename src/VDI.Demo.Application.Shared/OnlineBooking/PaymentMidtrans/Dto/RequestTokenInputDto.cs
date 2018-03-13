using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.PaymentMidtrans.Dto
{
    public class RequestTokenInputDto
    {
        public transactionDetailsDtos transaction_details { get; set; }
        public creditCards credit_card { get; set; }

    }
    public class creditCards
    {
        public bool? secure { get; set; }
    }

    public class transactionDetailsDtos
    {
        public int gross_amount { get; set; }
        public string order_id { get; set; }
    }
}
