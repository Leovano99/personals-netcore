using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.CustomerMember.Dto
{
    public class DetailCustomerMobileResultDto
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
        public string province { get; set; }
        public string city { get; set; }
        public string postCode { get; set; }
        public string number { get; set; }
        public string email { get; set; }
        public string marCode { get; set; }

        public string documentTypeKTP { get; set; }
        public string documentBinaryKTP { get; set; }

        public string documentTypeNPWP { get; set; }
        public string documentBinaryNPWP { get; set; }

        public string documentTypeKK { get; set; }
        public string documentBinaryKK { get; set; }

        public string documentTypeKTPPasangan { get; set; }
        public string documentBinaryKTPPasangan { get; set; }

        public string documentTypePassport { get; set; }
        public string documentBinaryPassport { get; set; }

        public string documentTypeAkte { get; set; }
        public string documentBinaryAkte { get; set; }

        public string documentTypeNPWPPerusahaan { get; set; }
        public string documentBinaryNPWPPerusahaan { get; set; }

        public string documentTypeKTPDireksi { get; set; }
        public string documentBinaryKTPDireksi { get; set; }

        public string documentTypeTDP { get; set; }
        public string documentBinaryTDP { get; set; }

        public string message { get; set; }

    }

    public class DataByte
    {
        public string documentBinarys { get; set; }
        public string documentType { get; set; }
    }
}
