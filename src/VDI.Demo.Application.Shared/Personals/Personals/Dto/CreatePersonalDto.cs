using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class CreatePersonalDto
    {
        public string psCode { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public DateTime? birthDate { get; set; }
        public string birthPlace { get; set; }
        public string marCode { get; set; }
        public string npwp { get; set; }
        public string relCode { get; set; }
        public string bloodCode { get; set; }
        public string occID { get; set; }
        public string nationID { get; set; }
        public string familyStatus { get; set; }
        public string FPTransCode { get; set; }
        public string grade { get; set; }
        public bool isActive { get; set; }
        public string remarks { get; set; }
        public bool isInstitute { get; set; }
    }
}
