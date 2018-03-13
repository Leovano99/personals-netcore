using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class GetKeyPeopleDto
    {
        public int Id { get; set; }
        public string psCode { get; set; }
        public int refID { get; set; }
        public int keyPeopleId { get; set; }
        public string keyPeopleDesc { get; set; }
        public string keyPeopleName { get; set; }
        public string keyPeoplePSCode { get; set; }
        public bool isAcive { get; set; }
        public string LastModificationTime { get; set; }
        public string LastModifierUserId { get; set; }
        public string CreationTime { get; set; }
        public string CreatorUserId { get; set; }
    }
}
