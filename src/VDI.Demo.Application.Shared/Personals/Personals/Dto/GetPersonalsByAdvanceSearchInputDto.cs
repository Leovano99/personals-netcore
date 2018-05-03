using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class GetPersonalsByAdvanceSearchInputDto
    {
        public string keyword { get; set; }
        public string idNumber { get; set; }
        public string memberCode { get; set; }
        public string birthDate { get; set; }
        public string email { get; set; }
    }
}
