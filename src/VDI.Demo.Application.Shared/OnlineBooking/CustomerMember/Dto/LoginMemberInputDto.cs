using Abp.Auditing;
using Abp.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VDI.Demo.OnlineBooking.CustomerMember.Dto
{
    public class LoginMemberInputDto
    {
        public string username { get; set; }

        public string password { get; set; }
    }
}
