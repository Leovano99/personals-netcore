using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class CreateFamilyDto
    {
        public string entityCode { get; set; }
        public string psCode { get; set; }
        public int refID { get; set; }
        public string familyName { get; set; }
        public string familyStatus { get; set; }
        public DateTime? birthDate { get; set; }
        public string occID { get; set; }
    }
}
