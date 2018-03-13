using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class CreatePaymentHeaderInputDto
    {
        public int entityID { get; set; }
        public int accountID { get; set; }
        public int? bookingHeaderID { get; set; }
        public int? payForID { get; set; }
        public DateTime? apvTime { get; set; }
        public string apvUn { get; set; }
        public DateTime? clearDate { get; set; }
        public string combineCode { get; set; }
        public string controlNo { get; set; }
        public bool hadMail { get; set; }
        public bool isSms { get; set; }
        public string ket { get; set; }
        public DateTime? mailTime { get; set; }
        public DateTime paymentDate { get; set; }
        public string transNo { get; set; }
        public DateTime? SMSSentTime { get; set; }
    }
}
