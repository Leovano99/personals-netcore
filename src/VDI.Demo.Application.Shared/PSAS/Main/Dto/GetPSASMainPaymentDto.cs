using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.PSAS.Term.Dto;

namespace VDI.Demo.PSAS.Main.Dto
{
    public class GetPSASMainPaymentDto
    {
        public string bankName { get; set; }
        public string bankBranch { get; set; }
        public string account { get; set; }
        public string transNo { get; set; }
        public DateTime PMTDate { get; set; }
        public TypeDto type { get; set; }
        public DateTime? clearDate { get; set; }
        public decimal netAmount { get; set; }
        public decimal vatAmt { get; set; }
        public string remarks { get; set; }
        public string taxFP { get; set; }
    }
}
