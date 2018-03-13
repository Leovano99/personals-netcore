using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.TR_KeyPeoples.Dto
{
    public class UpdateTrJKeyPeopleInputDto
    {
        public int Id { get; set; }
        public string psCode { get; set; }
        public int refID { get; set; }
        public int keyPeopleID { get; set; }
        public string keyPeopleName { get; set; }
        public string keyPeoplePSCode { get; set; }
    }
}
