using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class CreateCompanyDto
    {
        public string entityCode { get; set; }
        public string psCode { get; set; }
        public int refID { get; set; }
        public string coName { get; set; }
        public string coAddress { get; set; }
        public string coCity { get; set; }
        public string coPostCode { get; set; }
        public string coCountry { get; set; }
        public string coType { get; set; }
        public string jobTitle { get; set; }
    }
}
