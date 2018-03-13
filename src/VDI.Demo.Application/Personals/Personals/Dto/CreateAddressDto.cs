using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class CreateAddressDto
    {
        public string psCode { get; set; }
        public int refID { get; set; }
        public string addrType { get; set; }
        public string address { get; set; }
        public string postCode { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string Kelurahan { get; set; }
        public string Kecamatan { get; set; }
    }
}
