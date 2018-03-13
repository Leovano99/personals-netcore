using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class CreateKeyPeopleDto
    {
        public string psCode { get; set; }
        public int refID { get; set; }
        public int keyPeopleId { get; set; }
        public string keyPeopleName { get; set; }
        public string keyPeoplePSCode { get; set; }
    }
}
