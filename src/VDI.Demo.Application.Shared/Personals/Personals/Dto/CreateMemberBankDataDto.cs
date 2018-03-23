using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class CreateMemberBankDataDto
    {
        public string bankType { get; set; }
        public string bankCode { get; set; }
        public string bankAccNo { get; set; }
        public string bankAccName { get; set; }
        public string bankBranchName { get; set; }
        public int bankAccountRefID { get; set; }
    }
}
