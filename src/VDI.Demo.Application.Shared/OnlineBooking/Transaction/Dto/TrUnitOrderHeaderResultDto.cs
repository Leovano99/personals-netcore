using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class TrUnitOrderHeaderResultDto
    {
        public int unitOrderHeaderID { get; set; }

        public string memberCode { get; set; }

        public string memberName { get; set; }

        public string scmCode { get; set; }

        public bool result { get; set; }

        public string message { get; set; }
    }
}
