using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.CustomerMember.Dto
{
    public class ListCustomerResultDto
    {
        public string psCode { get; set; }
        public string name { get; set; }
        public DateTime? birthDate { get; set; }
        public string KTP { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string message { get; set; }
    }
}
