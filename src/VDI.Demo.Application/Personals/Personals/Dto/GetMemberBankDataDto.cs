using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class GetMemberBankDataDto
    {
        public string bankType { get; set; }
        public string bankCode { get; set; }
        public string bankAccNo { get; set; }
        public string bankAccName { get; set; }
        public string bankBranchName { get; set; }
    }
}
