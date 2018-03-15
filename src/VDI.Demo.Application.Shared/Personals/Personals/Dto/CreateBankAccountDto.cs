using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class CreateBankAccountDto
    {
        public string entityCode { get; set; }
        public string psCode { get; set; }
        public int refID { get; set; }
        public string BankCode { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public bool isAutoDebit { get; set; }
        public bool isMain { get; set; }
        public string BankBranchName { get; set; }
    }
}
