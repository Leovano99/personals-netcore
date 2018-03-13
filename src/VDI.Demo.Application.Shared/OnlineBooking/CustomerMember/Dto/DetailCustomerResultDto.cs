using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.CustomerMember.Dto
{
    public class DetailCustomerResultDto
    {
        public string psCode { get; set; }
        public string idType { get; set; }
        public string idNo { get; set; }
        public string name { get; set; }
        public string NPWP { get; set; }
        public DateTime? birthDate { get; set; }
        public string birthPlace { get; set; }
        public string sex { get; set; }
        public string nationID { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string postCode { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string marCode { get; set; }

        public string message { get; set; }


        public List<DocumentImage> documentImages { get; set; }
    }

    public class DocumentImage
    {
        public string documentType { get; set; }
        public string documentImage { get; set; }
    }
}
