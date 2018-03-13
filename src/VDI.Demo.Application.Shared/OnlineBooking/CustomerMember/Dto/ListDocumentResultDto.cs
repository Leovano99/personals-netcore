using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.CustomerMember.Dto
{
    public class ListDocumentResultDto
    {
        public string documentType { get; set; }
        public string document { get; set; }

        public string message { get; set; }
    }
    public class ListDocumentDto
    {
        public string documentType { get; set; }
        public string documentBinary { get; set; }
    }
}
