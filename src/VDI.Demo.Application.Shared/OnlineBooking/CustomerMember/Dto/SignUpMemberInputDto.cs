﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.CustomerMember.Dto
{
    public class SignUpMemberInputDto
    {
        public string memberCode { get; set; }

        public string email { get; set; }

        public string birthDate { get; set; }

        public string password { get; set; }

        public string confirmPassword { get; set; }

    }
}
