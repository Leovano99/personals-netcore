using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class CreateIDNumberDto
    {
        public string entityCode { get; set; }
        public string psCode { get; set; }
        public int refID { get; set; }
        public string idType { get; set; }
        public string idNo { get; set; }
        public DateTime? expiredDate { get; set; }
    }
}
