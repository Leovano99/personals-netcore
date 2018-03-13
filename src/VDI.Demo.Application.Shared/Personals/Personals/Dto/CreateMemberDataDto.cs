using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class CreateMemberDataDto
    {
        public string psCode { get; set; }
        public string scmCode { get; set; }
        public string memberCode { get; set; }
        public string parentMemberCode { get; set; }
        public string specCode { get; set; }
        public string CDCode { get; set; }
        public string ACDCode { get; set; }
        public string PTName { get; set; }
        public string PrincName { get; set; }
        public string spouName { get; set; }
        public string remarks1 { get; set; }
        public bool isCD { get; set; }
        public bool isACD { get; set; }
        public bool isInstitusi { get; set; }
        public bool isPKP { get; set; }
        public string franchiseGroup { get; set; }
    }
}
