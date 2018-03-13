using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VDI.Demo.OnlineBooking.Email.Dto
{
    public class SendEmailInputDto
    {
        public string toAddress { get; set; }

        public string subject { get; set; }

        public string body { get; set; }

        public string pathKP { get; set; }
        
    }
}
