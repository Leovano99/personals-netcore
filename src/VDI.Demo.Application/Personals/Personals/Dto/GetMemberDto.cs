using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class GetMemberDto
    {
        public GetMemberDataDto memberData { get; set; }
        public GetMemberActivationDto memberActivation { get; set; }
        public GetMemberBankDataDto memberBankData { get; set; }
    }
}
