using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class CreatePaymentDetailInputDto
    {
        public int entityID { get; set; }
        public string bankName { get; set; }
        public string chequeNo { get; set; }
        public DateTime dueDate { get; set; }
        public string ket { get; set; }
        public string othersTypeCode { get; set; }
        public int payNo { get; set; }
        public int payTypeID { get; set; }
        public int paymentHeaderID { get; set; }
        public string status { get; set; }
    }
}
