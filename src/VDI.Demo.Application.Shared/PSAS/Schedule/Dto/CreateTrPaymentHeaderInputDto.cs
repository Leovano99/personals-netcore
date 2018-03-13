using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.Schedule.Dto
{
    public class CreateTrPaymentHeaderInputDto
    {
        public int accountID { get; set; }
        public int? bookingHeaderID { get; set; }
        public int? payForID { get; set; }
    }
}
