using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class GetContactDto
    {
        public List<GetPhoneDto> getPhone { get; set; }
        public List<GetEmailDto> getEmail { get; set; }
        public List<GetAddressDto> getAddress { get; set; }
    }
}
