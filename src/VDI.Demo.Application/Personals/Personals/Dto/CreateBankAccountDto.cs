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
    }
}
