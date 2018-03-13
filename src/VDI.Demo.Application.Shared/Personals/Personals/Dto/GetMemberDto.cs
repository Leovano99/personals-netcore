using System;
using System.Collections.Generic;
using System.Text;
using static VDI.Demo.Personals.Personals.Dto.GetUniversalPersonalDto;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class GetMemberDto
    {
        public GetMemberDataDto memberData { get; set; }
        public GetMemberActivationDto memberActivation { get; set; }
        public GetMemberBankDataDto memberBankData { get; set; }
        public GetUpdateInfo UpdateInfo { get; set; }
    }
}
