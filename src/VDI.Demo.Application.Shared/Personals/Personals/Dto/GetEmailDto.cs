using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class GetEmailDto
    {
        public string entityCode { get; set; }
        public string psCode { get; set; }
        public int refID { get; set; }
        public string email { get; set; }
        public bool isValid { get; set; }
        public string LastModificationTime { get; set; }
        public string LastModificationBy { get; set; }
        public string CreatedBy { get; set; }
        public string CreateTime { get; set; }
    }
}
