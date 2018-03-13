using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.CustomerMember.Dto
{
    public class LoginMemberMobileDto
    {
        public string AccessToken { get; set; }
        public string psCode { get; set; }
        public string memberCode { get; set; }
        public string memberName { get; set; }
        public string scmCode { get; set; }
        public long userId { get; set; }
        public DateTime? birthDate { get; set; }
    }
}
