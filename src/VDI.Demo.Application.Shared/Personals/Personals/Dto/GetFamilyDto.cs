using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class GetFamilyDto
    {
        public string psCode { get; set; }
        public int refID { get; set; }
        public string familyName { get; set; }
        public string familyStatus { get; set; }
        public DateTime? birthDate { get; set; }
        public string occID { get; set; }
        public string occDesc { get; set; }
        public string LastModificationTime { get; set; }
        public string LastModifierUserId { get; set; }
        public string CreationTime { get; set; }
        public string CreatorUserId { get; set; }
    }
}
