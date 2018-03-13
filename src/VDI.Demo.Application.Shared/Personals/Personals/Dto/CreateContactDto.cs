using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class CreateContactDto
    {
        public List<CreatePhoneDto> inputPhone { get; set; }
        public List<CreateEmailDto> inputEmail { get; set; }
        public List<CreateAddressDto> inputAddress { get; set; }
    }
}
