using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class CreatePhoneDto
    {
        public string entityCode { get; set; }
        public string psCode { get; set; }
        public int refID { get; set; }
        public string phoneType { get; set; }
        public string number { get; set; }
        public string remarks { get; set; }
    }
}
