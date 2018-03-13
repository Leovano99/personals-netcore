using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class GetBankAccountDto
    {
        public string entityCode { get; set; }
        public string psCode { get; set; }
        public int refID { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string LastModificationTime { get; set; }
        public string LastModifierUserId { get; set; }
        public string CreationTime { get; set; }
        public string CreatorUserId { get; set; }
    }
}
