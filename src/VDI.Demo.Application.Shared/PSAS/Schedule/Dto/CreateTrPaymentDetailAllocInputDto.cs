using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.Schedule.Dto
{
    public class CreateTrPaymentDetailAllocInputDto
    {
        public int entityID { get; set; }
        public short schedNo { get; set; }
        public int paymentDetailID { get; set; }
        public decimal vatAmt { get; set; }
        public decimal netAmt { get; set; }
    }
}
