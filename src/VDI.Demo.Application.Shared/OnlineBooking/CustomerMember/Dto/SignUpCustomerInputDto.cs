using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.CustomerMember.Dto
{
    public class SignUpCustomerInputDto
    {
        public string idType { get; set; }
        public string idNo { get; set; }
        public string name { get; set; }
        public string NPWP { get; set; }
        public string birthDate { get; set; }
        public string birthPlace { get; set; }
        public string sex { get; set; }
        public string nationID { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string postCode { get; set; }
        public string number { get; set; }
        public string email { get; set; }
        public string marCode { get; set; }

        public List<DocumentUpload> document { get; set; }
    }

    public class DocumentUpload
    {
        public string documentType { get; set; }
        public string documentBinary { get; set; }
    }
    public class DocumentByte
    {
        public byte[] documentBinarys { get; set; }
        public string documentType { get; set; }
    }
}
