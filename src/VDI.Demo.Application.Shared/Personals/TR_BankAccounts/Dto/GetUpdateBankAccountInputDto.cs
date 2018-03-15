using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.TR_BankAccounts.Dto
{
    public class GetUpdateBankAccountInputDto
    {
        public string psCode { get; set; }
        public int refID { get; set; }
        public string bankCode { get; set; }
        public string AccNo { get; set; }
        public string AccName { get; set; }
        public bool isAutoDebit { get; set; }
        public bool isMain { get; set; }
        public string bankBranchName { get; set; }
    }
}
