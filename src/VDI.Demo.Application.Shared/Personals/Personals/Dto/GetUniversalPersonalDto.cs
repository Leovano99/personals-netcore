using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class GetUniversalPersonalDto
    {
        public GetPersonalDto personal { get; set; }
        public GetContactDto contact { get; set; }
        public List<GetAddressDto> address { get; set; }
        public List<GetKeyPeopleDto> keyPeople { get; set; }
        public List<GetBankAccountDto> bankAccount { get; set; }
        public List<GetCompanyDto> company { get; set; }
        public List<GetDocumentDto> document { get; set; }
        public List<GetIDNumberDto> idNumber { get; set; }
        public List<GetFamilyDto> family { get; set; }
        public List<GetMemberDto> member { get; set; }
        public GetUpdateInfo UpdateInfo { get; set; }

        public class GetUpdateInfo
        {
            public string updateBy { get; set; }
            public DateTime? updateTime { get; set; }
        }
    }
}
