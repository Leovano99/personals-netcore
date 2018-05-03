using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class GetPersonalsByKeywordList
    {
        public string psCode { get; set; }
        public string name { get; set; }
        public string birthDate { get; set; }
        public string modifTime { get; set; }
        public long? updatedBy { get; set; }
        public bool isInstitute { get; set; }
        public string idNo { get; set; }
        public string memberCode { get; set; }
        public string email { get; set; }
    }
}
