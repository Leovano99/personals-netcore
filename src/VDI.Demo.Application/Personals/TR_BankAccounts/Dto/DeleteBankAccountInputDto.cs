using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.TR_BankAccounts.Dto
{
    public class DeleteBankAccountInputDto
    {
        public string psCode { get; set; }
        public int refID { get; set; }
        public string bankCode { get; set; }
    }
}
