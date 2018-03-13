using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class CreateTrJournalInputDto
    {
        public string entityCode { get; set; }
        public string journalCode { get; set; }
        public string COACodeFIN { get; set; }
        public string COACodeAcc { get; set; }
        public DateTime journalDate { get; set; }
        public decimal debit { get; set; }
        public decimal kredit { get; set; }
        public string remarks { get; set; }
    }
}
