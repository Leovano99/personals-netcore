using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class GetMemberActivationDto
    {
        public string memberStatusCode { get; set; }
        public bool isMember { get; set; }
        public bool isActive { get; set; }
        public string password { get; set; }
    }
}
