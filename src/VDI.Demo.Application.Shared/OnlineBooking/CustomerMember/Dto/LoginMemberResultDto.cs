using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.CustomerMember.Dto
{
    public class LoginMemberResultDto
    {
        public string psCode { get; set; }
        public string memberCode { get; set; }
        public string memberName { get; set; }
        public string scmCode { get; set; }
        public int userRef { get; set; }
        public DateTime? birthDate { get; set; }
        
    }
}
