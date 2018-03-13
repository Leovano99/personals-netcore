using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class CreateMemberDto
    {
        public CreateMemberDataDto memberData { get; set; }
        public CreateMemberActivationDto memberActivation { get; set; }
        public CreateMemberBankDataDto memberBankData { get; set; }
    }
}
