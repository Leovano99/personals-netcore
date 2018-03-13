using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.TR_Emails.Dto
{
    public class GetUpdateEmailInputDto
    {
        public string entityCode { get; set; }
        public string psCode { get; set; }
        public int refID { get; set; }
        public string email { get; set; }
    }
}
