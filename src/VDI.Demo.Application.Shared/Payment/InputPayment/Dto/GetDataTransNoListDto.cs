using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class GetDataTransNoListDto
    {
        public int paymentHeaderID { get; set; }
        public string transNo { get; set; }
        public int? payForID { get; set; }
        public string payFor { get; set; }
        public DateTime payDate { get; set; }
        public int? bookingHeaderID { get; set; }
        public string bookCode { get; set; }
        public int unitID { get; set; }
        public int unitCodeID { get; set; }
        public string unitNo { get; set; }
        public string unitCode { get; set; }
        public string clientName { get; set; }
        public decimal amount { get; set; }
        public string remarks { get; set; }
    }
}
