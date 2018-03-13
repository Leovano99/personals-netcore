using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.CustomerMember.Dto
{
    public class ListCustomerInputDto
    {
        public string name { get; set; }
        public DateTime? birthDate { get; set; }
        public string idNo { get; set; }
    }
}
