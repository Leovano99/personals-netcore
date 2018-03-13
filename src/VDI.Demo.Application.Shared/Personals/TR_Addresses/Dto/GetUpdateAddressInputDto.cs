using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.TR_Addresses.Dto
{
    public class GetUpdateAddressInputDto
    {
        public string psCode { get; set; }
        public int refID { get; set; }
        public string addrType { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string kelurahan { get; set; }
        public string kecamatan { get; set; }
        public string postCode { get; set; }
        public string country { get; set; }
    }
}
