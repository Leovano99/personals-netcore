using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.PaymentMidtrans.Dto
{
    public class PaymentOnlineBookingResponse
    {
        public string id { get; set; }
        public string status_code { get; set; }
        public string status_message { get; set; }
        public string transaction_id { get; set; }
        public string order_id { get; set; }
        public string gross_amount { get; set; }
        public string payment_type { get; set; }
        public string transaction_time { get; set; }
        public string transaction_status { get; set; }
        public string fraud_status { get; set; }
        //public string redirect_data { get; set; }
        public List<vaNumberPaymentDto> va_numbers { get; set; }
        public string permata_va_number { get; set; }
        public string redirect_url { get; set; }
        public string approval_code { get; set; }
        public string bill_key { get; set; }
        public string biller_code { get; set; }
        public string signature_key { get; set; }
        public string error_messages { get; set; }
        public List<string> validation_messages { get; set; }
    }
    public class vaNumberPaymentDto
    {
        public string bank { get; set; }
        public string va_number { get; set; }
    }

}
