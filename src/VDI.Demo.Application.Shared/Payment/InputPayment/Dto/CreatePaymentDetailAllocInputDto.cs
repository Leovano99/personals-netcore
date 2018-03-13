using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class CreatePaymentDetailAllocInputDto
    {
        public int entityID { get; set; }
        public decimal netAmt { get; set; }
        public int paymentDetailID { get; set; }
        public short schedNo { get; set; }
        public decimal vatAmt { get; set; }
    }
}
