using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class GetAddressDto
    {
        public string entityCode { get; set; }
        public string psCode { get; set; }
        public int refID { get; set; }
        public string addrType { get; set; }
        public string addrTypeName { get; set; }
        public string address { get; set; }
        public string postCode { get; set; }
        public string city { get; set; }
        public string cityCode { get; set; }
        public string country { get; set; }
        public string Kelurahan { get; set; }
        public string Kecamatan { get; set; }
        public string province { get; set; }
        public string provinceCode { get; set; }
        public string LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public string CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
    }
}
